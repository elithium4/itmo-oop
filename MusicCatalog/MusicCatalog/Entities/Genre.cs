﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Entities
{
    internal class Genre : IHasName
    {
        public int Id { get; set; }
        public string Name {  get; set; }

    }
}