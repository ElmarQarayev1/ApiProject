using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Flower.Core.Entities
{
    public class Slider:BaseEntity
    {
              
            public string Title { get; set; }
                    
            public string Desc { get; set; }

            public string ImageName { get; set; }

            public int Order { get; set; }
        
    }
}

