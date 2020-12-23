using Microsoft.Extensions.Logging;
using Nintex.Services.ShorteningURL.KeysGenerationService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintex.NetCore.Component.Utilities.Logger;


namespace Nintex.Services.ShorteningURL.KeysGenerationService.Service
{
    public class KGS : IKGS
    {
        private Dictionary<char, int> chars;
        private Counters counters;        
        private List<List<char>> listofChars;
        private readonly IKGSDBService _kGSDBService;
        private readonly ILogger<KGS> _logger;
        public KGS(IKGSDBService kGSDBService, ILogger<KGS> logger)
        {
            _kGSDBService = kGSDBService;
            _logger = logger;
            chars = new Dictionary<char, int>();
            InitializeCounter();
            InitializeStaticBase64Arrays();
           
            //for (int i = 0; i < listofChars.Count; i++)
            //    chars.Add(listofChars[i], i);
        }
        private void InitializeCounter()
        {
            var lastCounter = _kGSDBService.GetLatestKGSRound().Result;
            if (lastCounter == null)
                counters = new Counters(new List<int> { 0, 0, 0, 0, 0, -1 });
            else            
                counters =new Counters(lastCounter.LastCounter.Split(',').Select(e => int.Parse(e.Trim())).ToList());            
        }
        private void InitializeStaticBase64Arrays()
        {
            listofChars = new List<List<char>>();
            listofChars.Add("rpKQ5wg6Mf0yEx8hsdOWSB9zZnUHtqcjiGAemu1L+PavXlIND24/VbFYTR73CokJ".ToList()); // Randomized base64 chars 
            listofChars.Add("OZXlLjSFymfEQMT3HRuprWwVAb8ntk1C+/Po6cBxsDJYiU794ha0gqIKvzdG5e2N".ToList()); // Randomized base64 chars 
            listofChars.Add("IxdsJkSr7Nj1wME5phaZm809AyRfUnFgOCeuVzKcDYib3P62LGT/qXH4oW+vQtBl".ToList()); // Randomized base64 chars 
            listofChars.Add("uxNBHp6cbvmFSsijLatlyZRTGwz4gCd53oqW0JX/O9QUf7ePrMnYkDK8h+VI21AE".ToList()); // Randomized base64 chars 
            listofChars.Add("cJYu1Q0AL7yrkGBengjWtVviU/zSbdOKD95HaoEP6f+2MNhpmITqxC3l8XFZs4Rw".ToList()); // Randomized base64 chars 
            listofChars.Add("l0Y4S2ntVKaRhGdZicJHwM3e+Cmjxrvu5sWyB6zk/XNTQbqEpLPDUA8gf1oF97OI".ToList()); // Randomized base64 chars             
        }
        public IEnumerable<string> GenerateNewKeys(int size)
        {
            var currentCounter=counters.Current();
            List<string> list = new List<string>();
            for (int n = 0; n < size; n++)
            {
                try
                {
                    list.Add(GenerateNewKey());
                }
                catch(Exception exception)
                {
                    _logger.LogError(exception,"Error while generating Keys");
                    break;
                }                
            }
            try
            {
                _kGSDBService.InsertNewRound(new Kgsround() { FromKey = list.First(), ToKey = list.Last(), LastCounter = counters.ToString(), RoundDate = DateTime.Now });
            }
            catch(Exception ex2)
            {
                counters.Reset(currentCounter);
                throw ex2;
            }            
            return list;

        }
        public string GenerateNewKey()
        {
            var l = counters.PlusOne();
            StringBuilder base64Text = new StringBuilder();
            for(int i=0;i<l.Count;i++)
                base64Text.Append(listofChars[i][l[i]]);
            return base64Text.ToString();
        }
    }
}
