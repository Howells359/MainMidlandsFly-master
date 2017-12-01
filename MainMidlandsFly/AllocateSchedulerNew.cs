using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMidlandsFly
{
    public class AllocateSchedulerNew
    {
        public static void Start()
        {
            new AllocateGroundCrewJobNew().Execute();

        }
    }
}
