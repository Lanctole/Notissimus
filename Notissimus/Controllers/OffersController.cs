using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notissimus.Models;

namespace Notissimus.Controllers
{
    public class OffersController : Controller
    {
        private readonly NotissimusDbContext _context;

        public OffersController( NotissimusDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://partner.market.yandex.ru/pages/help/YML.xml");
                var content = await response.Content.ReadAsStringAsync();

                var xml = XElement.Parse(content);
                var offers = xml.Descendants("offer").ToList();
                var tempOffer = offers.FirstOrDefault(o => (int)o.Attribute("id") == 12344);

                if (tempOffer != null)
                {
                    Offer offer = new Offer();
                    offer.Id=(int)tempOffer.Attribute("id");
                    offer.Type = tempOffer.Attribute("type").ToString();
                    if (tempOffer.Attribute("bid") != null)
                        offer.Bid = (int)tempOffer.Attribute("bid");
                    if (tempOffer.Attribute("cbid") != null)
                        offer.CBid = (int)tempOffer.Attribute("cbid");
                    offer.Available = (bool)(tempOffer.Attribute("available"));

                    int startIndex = tempOffer.ToString().IndexOf(">") + 1;
                    int endIndex = tempOffer.ToString().LastIndexOf("</offer>");
                    offer.OtherOfferProperties=tempOffer.ToString().Substring(startIndex, endIndex - startIndex);
                    
                    if (!_context.Offers.Any(o => o.Id == offer.Id))
                    {
                        _context.Add(offer);
                        await _context.SaveChangesAsync();
                    }
                }

                var offerFromDb = await _context.Offers.FirstOrDefaultAsync(o => o.Id == 12344);
                return View(offerFromDb);
            }
        }
    }
}
