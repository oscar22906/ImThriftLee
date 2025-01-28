using UnityEngine;
// Controlls all body part renderers, calling methods from all or one.
// Important - Player animation needs to reset to first frame when clothing is set.
public class OutfitManager : MonoBehaviour
{
    [Tooltip("Contains all body renderers to apply clothing")]
    public BodyPartRenderer[] bodyPartRenderers;
    [SerializeField] OutfitSet currentSet;
    int currentFrame;

    public void UpdateOutfit()
    {
        foreach (BodyPartRenderer bpr in bodyPartRenderers)
        {
            bpr.UpdateClothing(currentFrame);
        }
    }

    public void SetOutfit(Clothing newClothing)
    {
        foreach (BodyPartRenderer bpr in bodyPartRenderers)
        {
            bpr.ApplySet(newClothing.outfitSet);
            currentSet = newClothing.outfitSet;
        }
    }

    public void EquipClothing(Clothing newClothing)
    {
        foreach (BodyPartRenderer bpr in bodyPartRenderers)
        {
            if (newClothing.bodyPart == bpr.bodyPart)
            {
                bpr.SetClothing(newClothing);
            }
        }
    }

    public void ResetOutfit() // Sets all sprites to null
    {
        foreach (BodyPartRenderer bpr in bodyPartRenderers)
        {
            bpr.ResetClothing();
        }
    }


        //switch (newClothing.bodyPart)
        //{
        //    case BodyPart.Head:

        //        break;
        //    case BodyPart.Torso:

        //        break;
        //    case BodyPart.Legs:

        //        break;
        //    case BodyPart.Feet:

        //        break;
        //    default:
        //        Debug.Log("Clothing doesn't have a BodyPart set");
        //        break;
        //}
}
