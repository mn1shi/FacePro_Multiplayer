using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class AvatarSync : RealtimeComponent<AvatarSyncModel>
{
    public List<GameObject> _avatarList;
    private int _avatarIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void OnRealtimeModelReplaced(AvatarSyncModel previousModel, AvatarSyncModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.avatarDidChange -= AvatarDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.avatar = _avatarIndex;

            // Update the mesh render to match the new model
            UpdateAvatarIndex();

            // Register for events so we'll know if the color changes later
            currentModel.avatarDidChange += AvatarDidChange;
        }
    }

    private void AvatarDidChange(AvatarSyncModel model, int value)
    {
        // Update the mesh renderer
        UpdateAvatarIndex();

    }

    private void UpdateAvatarIndex()
    {
            // Get the color from the model and set it on the mesh renderer.
            _avatarIndex = model.avatar;

            foreach (var avatar in _avatarList)
            {
                avatar.SetActive(false);
            }

            _avatarList[_avatarIndex].SetActive(true);
    }

    public void SetAvatarIndex(int index)
    {
        model.avatar = index;
    }

}
