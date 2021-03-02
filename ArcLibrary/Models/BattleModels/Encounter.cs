using ArcLibrary.DataModels.CharacterModels;
using ArcLibrary.Models.NPCModels;
using System.Collections.Generic;

namespace ArcLibrary.Models.BattleModels
{
    public class Encounter
    {
        public int EncounterId { get; set; }
        public List<Sheet> Players { get; set; }
        public List<Enemy> Enemies { get; set; }
        public List<Boss> Bosses { get; set; }
    }
}
