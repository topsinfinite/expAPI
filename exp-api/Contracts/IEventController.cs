// Template: Controller Interface (ApiControllerInterface.t4) version 1.1

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using iedge.exp_api.Models;


namespace iedge.exp_api
{
    public interface IEventController
    {

        IHttpActionResult Get([FromUri] string deviceid);
    }
}
