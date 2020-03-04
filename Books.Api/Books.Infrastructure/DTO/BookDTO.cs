﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Infrastructure.DTO
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string PublishingCompany { get; set; }
        public string Description { get; set; }
        public int Pages { get; set; }
    }
}
