﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    public class MMLInfo
    {
        public string MacroName { get; set; } = "";

        public string SampleLyric { get; set; } = "";

        public string NoteLen { get; set; } = "";

        public Boolean WithDot { get; set; } = false;

        public int Tempo { get; set; }

        public int Octave { get; set; }

        public int Key { get; set; }
    }
}
