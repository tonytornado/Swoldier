using Microsoft.AspNetCore.Mvc;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OCFX_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RPGController : ControllerBase
    {
        public RPGController(OCFXContext context) => Context = context ?? throw new ArgumentNullException(nameof(context));

        public OCFXContext Context { get; }

        /// <summary>
        /// Gets a list of all the campaigns
        /// </summary>
        /// <returns></returns>
        [HttpGet("Campaigns")]
        public List<Campaign> GetCampaigns() => Context.Campaigns.ToList();

        /// <summary>
        /// Gets all the quests
        /// </summary>
        /// <returns></returns>
        [HttpGet("Quests")]
        public List<Quest> GetQuests() => Context.Quests.ToList();
    }
}