// Template: Controller Implementation (ApiControllerImplementation.t4) version 1.1

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using exp_api;
using iedge.exp_api.Model;
using System;


namespace iedge.exp_api
{
	public partial class EventController : IEventController
	{
		CustomerFeedbackGenericDBEntities db = new CustomerFeedbackGenericDBEntities();
		/// <summary>
		/// Entity representing a event. Get event configuration.
		/// </summary>
		/// <param name="deviceid">This is the device id</param>
		/// 
		[HttpGet]
		[Route("getevent")]
		public IHttpActionResult GetEvent([FromUri] string eventcode)
		{
			var msg=new Model.ResponseMessage();
			if (!string.IsNullOrEmpty(eventcode))
			{
				var evt = db.Events.FirstOrDefault(d => d.Code == eventcode);
				if (evt != null)
				{
					if(evt.isActive.HasValue && evt.isActive.Value)
					{
						
						if(evt.ValidTill.HasValue && evt.ValidTill.Value.Date>=DateTime.Now.Date)
						{
							msg.message = "000";//000 Event is valid
							msg.message = "Event is valid";
						}else
						{
							msg.message = "103";//103 Event has expired
							msg.message = "Event has expired!!!";
						}

					}else
					{
						msg.code = "002";//002 event not active
						msg.message = "Event is not active!!!";
					}

				}
				else
				{
					msg.code = "001";//001 code not found
					msg.message = "Event code does not exist!!!";
				}
				return Ok(msg);
			}
			return BadRequest();
		}
		 
		public IHttpActionResult Get([FromUri] string deviceid)
		{

			// TODO: implement Get - route: event/
		 //   var request = Request;
		  //  var headers = request.Headers;
			//get deviceId
		  
			if (!string.IsNullOrEmpty(deviceid))
			{

				//var deviceInfo = db.DeviceTbls.FirstOrDefault(dtbl => dtbl.DeviceID == deviceid);
				//var eventInfo = db.Events.FirstOrDefault(dtbl => dtbl.Code == deviceid);
			   // if (eventInfo != null)
				//{
					var m_event = new Model.Event();
					//var available = db.EventDevices.FirstOrDefault(ed => ed.DeviceID == deviceInfo.ID);
					var available = db.Events.FirstOrDefault(ed => ed.Code.ToUpper().Trim() == deviceid.ToUpper().Trim());

					if (available != null)
					{
						//get event info 
						
						//get event question
						//var question = db.Events.SingleOrDefault(e => e.ID == available.ID);
						var question = available;
						var c_event = db.HomeSettings.Where(e => e.EventID == available.ID);
						if (c_event.Any())
						{
							var settings = new Settings
							{
								id = c_event.First().EventID.ToString(),
								title = (question != null) ? question.Question:"",
								smilies = Smilies(c_event)
							};
							m_event.settings = settings;
						}
						else
						{
							db.Dispose();
							return BadRequest();
						}
					   
						//event smilies
						//event metiric info (including elements)
					   
						var metricList = MetricInfo((int)available.ID);
						m_event.metrics = metricList;

					   // string result = Newtonsoft.Json.JsonConvert.SerializeObject(m_event);
						db.Dispose();
						return Ok(m_event);
					}
					else
					{
						db.Dispose();
						return BadRequest();
					}
				   
				//}

				//return BadRequest();


			}
			db.Dispose();
			return BadRequest();
		}

		private IList<Model.Metric> MetricInfo(int eventId)
		{
			//get all metrics for event
			IList<Metric> metricList = new List<Metric>();
			var metrics = db.EventMetrics.Where(ev => ev.EventID == eventId && ev.isActive==true);
			foreach (var metric in metrics)
			{
				var realmetric = new Metric
				{
					icon = metric.Icon,
					id = metric.ID.ToString(),
					label = metric.Label,
					title = metric.Title 
				};

				var elements = db.MetricElements.Where(m => m.EventMetricID == metric.ID && m.isActive==true);
				List<Element> elenList = new List<Element>();
				//get event element for metric
				if (elements.Any())
				{
					foreach (var element in elements)
					{
						var realelement = new Element
						{
							icon = element.Icon,
							id = element.ID.ToString(),
							note = element.Note,
							title = element.Title,
							metricid = metric.ID.ToString( )
						};
						elenList.Add(realelement);
					   
					}
				}
				realmetric.elements = elenList;
				metricList.Add(realmetric);
			}
			return metricList;
		}

		private IList<Smiley> Smilies(IQueryable<HomeSetting> eventSettings)
		{
			IList<Smiley> smilies = new List<Smiley>();

			if (eventSettings.Any())
			{
				int x = 1;
				foreach (var settting in eventSettings)
				{
				   
						smilies.Add(new Smiley
						{
							//id = x.ToString(),//settting.ID.ToString(),
							id = settting.ID.ToString(),
							image = settting.SmileyImage,
							label = settting.Label,
							type = (int)settting.SmileyType
						});
					x++;
				}
			}
			return smilies;
		}

	}
}
