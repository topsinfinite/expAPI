// Template: Controller Interface (ApiControllerInterface.t4) version 1.1

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using iedge.exp_api.Models;


namespace iedge.exp_api
{
    public interface IFeedbackController
    {

        IHttpActionResult Post(Models.FeedbackPostRequestContent feedbackpostrequestcontent,[FromUri] string deviceid,[FromUri] string eventid);

        IHttpActionResult Post(Models.FeedbackPostRequestContent feedbackpostrequestcontent);

    }
}
