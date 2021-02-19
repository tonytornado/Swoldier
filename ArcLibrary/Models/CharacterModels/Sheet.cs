using SocialLibrary.Profile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArcLibrary.DataModels.CharacterModels
{
    public class Sheet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Backstory { get; set; }
        public string Bonds { get; set; }

        // Points
        public int Lp { get; set; }
        public int MaxLp { get; set; }
        public int O2 { get; set; }
        public int MaxO2 { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int HP { get; set; }

        // Aspects
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Int { get; set; }
        public int Mot { get; set; }
        public int Cha { get; set; }

        // Character Skills and Traits
        public ClassBuild FitClass { get; set; }
        public MorphBuild FitBuild { get; set; }

        // Equipment
        public List<Skill> SkillSet { get; set; }
        public List<Accessory> Equipment { get; set; }

        // Add it to a profile
        public int AssociatedProfileId { get; set; }
        [ForeignKey("AssociatedProfileId")]
        public ProfileData AssociatedProfile { get; set; }

        // ATK and DEF values
        private int DefenseValue => SetDefenseValue(Con);
        private int AttackValue => SetAttackValue(Str, Dex);
        private int ArmorValue => SetArmorValue(Equipment);

        private static int SetArmorValue(List<Accessory> equipment)
        {
            int armor = 0;
            foreach (Accessory item in equipment)
            {
                armor += item.ConMod;
            }
            return armor;
        }

        private static int SetDefenseValue(int con)
        {
            var BaseDefense = con / 2;
            return BaseDefense;
        }

        private static int SetAttackValue(int str, int dex)
        {
            var BaseAttack = str / 2 + (dex / 3);
            return BaseAttack;
        }

        public string Attack(int RollValue, Sheet target)
        {
            var rand = new Random();
            var hit = rand.Next((AttackValue/3), AttackValue);

            if(RollValue != 0)
            {
                hit *= RollValue / 18;
            } else if(RollValue == 20)
            {
                hit *= 2;
            } else if(RollValue == 0)
            {
                hit = 1;
            }

            // Calculate attack vs target defense
            hit -= (target.DefenseValue + target.ArmorValue);
            target.HP -= hit;
            return $"{Name} attacked {target.Name} and dealt {hit} damage!";
        }

        public string Deflect(int RollValue, Sheet target)
        {
            var rand = new Random();
            var block = rand.Next((DefenseValue / 3), DefenseValue);

            if (RollValue != 0)
            {
                block *= RollValue / 18;
            }
            else if (RollValue == 20)
            {
                block *= 2;
            }
            else if (RollValue == 0)
            {
                block = 1;
            }

            block -= (target.AttackValue - ArmorValue);
            target.HP += block;
            return $"{Name} blocked {target.Name} and mitigated {block} damage!";
        }
    }
}