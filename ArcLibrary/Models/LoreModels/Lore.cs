using CoreLibrary.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArcLibrary.Models.LoreModels
{
    public class Lore
    {
        [Key]
        public int LoreId { get; set; }
        public string Title { get; set; }
        [StringLength(300,MinimumLength = 100)]
        public string Description { get; set; }
        
        /// <summary>
        /// Writes a small portion of the <see cref="Description"/> property
        /// </summary>
        public string Excerpt => $"{Description?.Substring(0, 100)}...";

        public int LoreTypeId { get; set; }
        [ForeignKey("LoreTypeId")]
        public LoreType LoreType { get; set; }
        public List<LoreTag> LoreTags { get; set; }
    }

    public class LoreTag : Tag
    {
    }
}