﻿using System;
using System.Collections.Generic;

namespace LibraryApp.Models
{
    public partial class Authorship
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }

        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
