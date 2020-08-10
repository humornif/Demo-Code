using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.Http;

namespace demo
{
    public class DosAttackMiddleware
    {
        private static Dictionary<string, short> _IpAdresses = new Dictionary<string, short>();
        private static Stack<string> _Banned = new Stack<string>();
        private static Timer _Timer = CreateTimer();
        private static Timer _BannedTimer = CreateBanningTimer();

        private const int BANNED_REQUESTS = 10;
        private const int REDUCTION_INTERVAL = 1000; // 1 second    
        private const int RELEASE_INTERVAL = 5 * 60 * 1000; // 5 minutes    
        private RequestDelegate _next;

        public DosAttackMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            string ip = httpContext.Connection.RemoteIpAddress.ToString();

            if (_Banned.Contains(ip))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }

            CheckIpAddress(ip);

            await _next(httpContext);
        }
 
        private static void CheckIpAddress(string ip)
        {
            if (!_IpAdresses.ContainsKey(ip))
            {
                _IpAdresses[ip] = 1;
            }
            else if (_IpAdresses[ip] == BANNED_REQUESTS)
            {
                _Banned.Push(ip);
                _IpAdresses.Remove(ip);
            }
            else
            {
                _IpAdresses[ip]++;
            }
        }


        private static Timer CreateTimer()
        {
            Timer timer = GetTimer(REDUCTION_INTERVAL);
            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            return timer;
        }

        private static Timer CreateBanningTimer()
        {
            Timer timer = GetTimer(RELEASE_INTERVAL);
            timer.Elapsed += delegate {
                if (_Banned.Any()) _Banned.Pop();
            };
            return timer;
        }
 
        private static Timer GetTimer(int interval)
        {
            Timer timer = new Timer();
            timer.Interval = interval;
            timer.Start();
            return timer;
        }

        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            foreach (string key in _IpAdresses.Keys.ToList())
            {
                _IpAdresses[key]--;
                if (_IpAdresses[key] == 0) _IpAdresses.Remove(key);
            }
        }
    }
}
