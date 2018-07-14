using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.Controllers
{
    
    public class Joined : Controller
    {
        public string WhenJoined(TimeSpan timespan, int months, int days, int weeks)
    {
        if(months > 0 && months < 12)
        {
            return @months + " months and " + @days + " days ago";
        }

        else if(weeks >  0 && weeks < 4)
        {
            if(weeks == 1)
            {
                return @weeks + "week ago";
            }
            else
            {
                return @weeks + " weeks ago";
            }
        }

        else if(timespan.Days >  0 && timespan.Days < 8)
        {
            if(timespan.Days == 1)
            {
                return @timespan.Days + " day ago";
            }
            else
            {
                return @timespan.Days + " days ago";
            }
            
        }

        else if(timespan.Hours >  0 && timespan.Hours < 24)
        {
            return @String.Format("{0} hours and {1} minutes ago",timespan.Hours, timespan.Minutes);
        }
        else if(timespan.Minutes >  0 && timespan.Minutes < 60)
        {
            return @String.Format("{0} minutes ago",timespan.Minutes);
        }
        return "less than a minute";
       
       
        
    }
   
}
    }


