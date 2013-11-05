﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KeyHub.Data;
using KeyHub.Web.Models;
using KeyHub.Web.ViewModels;

namespace KeyHub.Web.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly IDataContextFactory dataContextFactory;
        public ControllerBase(IDataContextFactory dataContextFactory)
        {
            this.dataContextFactory = dataContextFactory;
        }

        protected Model.User UserEntity
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (var context = dataContextFactory.Create())
                    {
                        return (from x in context.Users where x.UserName == User.Identity.Name select x)
                                .Include(x => x.Rights).FirstOrDefault();
                    }
                }
                return null;
            }
        }
    }
}