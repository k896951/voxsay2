using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    public class NoteInfo
    {
        public int Key { get; set; }

        public string Note { get; set; } = "";

        public string NoteLen { get; set; } = "";

        public double Ticks { get; set; }

        public int Tempo { get; set; }

        public List<SubNoteInfo>? Notes { get; set; } = null;
    }
}
