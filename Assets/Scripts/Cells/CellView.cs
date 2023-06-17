using TMPro;
using UnityEngine;

namespace Game.Cells
{
    public class CellView : MonoBehaviour
    {
        [field: SerializeField] 
        public SpriteRenderer SpriteRenderer { get; private set; }
        
        [field: SerializeField] 
        public TMP_Text Coordinate { get; private set; }
    }
}
