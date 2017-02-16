// Template: Controller Implementation (ApiControllerImplementation.t4) version 1.1

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using exp_api;
using iedge.exp_api.Model;
using iedge.exp_api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace iedge.exp_api
{
    public partial class FeedbackController : IFeedbackController
    {
        private readonly CustomerFeedbackGenericDBEntities db = new CustomerFeedbackGenericDBEntities();

        /// <summary>
        /// Entity representing a feedback. Post a feedback
        /// </summary>
        /// <param name="feedbackpostrequestcontent"></param>
        /// <param name="deviceid">This is the device id</param>
        /// <param name="eventid">This is the event id</param>
        public IHttpActionResult Post([FromBody] Models.FeedbackPostRequestContent feedbackpostrequestcontent,
            [FromUri] string deviceid, [FromUri] string eventid)
        {
            // TODO: implement Post - route: feedback/

    
            {

                var customer = new Customer
                {
                    CustomerName = feedbackpostrequestcontent.Eventfeedback.Fullname,
                    MobileNo = feedbackpostrequestcontent.Eventfeedback.Phone,
                    Email = feedbackpostrequestcontent.Eventfeedback.Email,
                    EventID = int.Parse(feedbackpostrequestcontent.Eventfeedback.Eventid),
                    Comment = feedbackpostrequestcontent.Eventfeedback.Comment,
                    Gender = feedbackpostrequestcontent.Eventfeedback.Gender,
                    FeedBackVal = feedbackpostrequestcontent.Eventfeedback.Feedbackval,
                    DateCreated = DateTime.Parse(feedbackpostrequestcontent.Eventfeedback.DateAdded)
                };
                db.Customers.Add(customer);
                db.SaveChanges();

                if (feedbackpostrequestcontent.Feedbacks != null)
                {
                    foreach (FeedbackItem fb in feedbackpostrequestcontent.Feedbacks)
                    {
                        // Model.FeedBack fb = JsonConvert.DeserializeObject<iedge.exp_api.Model.FeedBack>(fback);
                        var e_feedback = new EventFeedback
                        {
                            DateAdded = DateTime.Parse(feedbackpostrequestcontent.Eventfeedback.DateAdded),
                            EventID = int.Parse(fb.eventid),
                            EventMetricID = int.Parse(fb.metricid),
                            MetricElementtID = int.Parse(fb.elementid),
                            Feedbackval = int.Parse(fb.feedbackval),
                            SmileyID = fb.smileyid,
                            SmileyType = fb.smileytype,
                            CustomerID = customer.ID
                        };
                        db.EventFeedbacks.Add(e_feedback);
                    }
                }

                db.SaveChanges();
            }
            return new System.Web.Http.Results.OkResult(new HttpRequestMessage(new HttpMethod("POST"), ""));
       
        }

        public IHttpActionResult Post([FromBody] Models.FeedbackPostRequestContent feedbackpostrequestcontent)
        {
            // TODO: implement Post - route: feedback/

            //HttpContent requestContent = Request.Content;
            //  string content =   requestContent.ReadAsStringAsync().Result;
            {
                //store customer data

                var customer = new Customer
                {
                    CustomerName = feedbackpostrequestcontent.Eventfeedback.Fullname,
                    MobileNo = feedbackpostrequestcontent.Eventfeedback.Phone,
                    Email = feedbackpostrequestcontent.Eventfeedback.Email,
                    EventID = int.Parse(feedbackpostrequestcontent.Eventfeedback.Eventid),
                    Comment = feedbackpostrequestcontent.Eventfeedback.Comment,
                    Gender = feedbackpostrequestcontent.Eventfeedback.Gender,
                    FeedBackVal = feedbackpostrequestcontent.Eventfeedback.Feedbackval,
                    DateCreated = DateTime.Parse(feedbackpostrequestcontent.Eventfeedback.DateAdded)
                };
                if (!string.IsNullOrEmpty(customer.CustomerName) || !string.IsNullOrEmpty(customer.MobileNo) || !string.IsNullOrEmpty(customer.Email) || !string.IsNullOrEmpty(customer.Comment))
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                }
                

                if (feedbackpostrequestcontent.Feedbacks != null)
                {
                    foreach (FeedbackItem fb in feedbackpostrequestcontent.Feedbacks)
                    {
                        // Model.FeedBack fb = JsonConvert.DeserializeObject<iedge.exp_api.Model.FeedBack>(fback);
                        var e_feedback = new EventFeedback
                        {
                            DateAdded = DateTime.Parse(feedbackpostrequestcontent.Eventfeedback.DateAdded),
                            EventID = int.Parse(fb.eventid),
                            EventMetricID = int.Parse(fb.metricid),
                            MetricElementtID = int.Parse(fb.elementid),
                            Feedbackval = int.Parse(fb.feedbackval),
                            SmileyID = fb.smileyid,
                            SmileyType = fb.smileytype,
                             
             
                        };
                        if (customer.ID > 0)
                        {
                            e_feedback.CustomerID = customer.ID;
                        }
                        db.EventFeedbacks.Add(e_feedback);
                    }
                }

                db.SaveChanges();
            }
            return new System.Web.Http.Results.OkResult(new HttpRequestMessage(new HttpMethod("POST"), ""));
        }

      
    }
}
