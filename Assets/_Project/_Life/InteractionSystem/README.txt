Place the Interactor class on player's camera. Only one Interactor should be active at a time.
The Interactable class is to be placed in interactable objects.
The selection UnityEvents on Interactable should be used locally on the interactable object (i.e. for displaying diegetic UI, or highlighting). The trigger event should be used with gameplay logic scripts to being actual interaction, like using devices, picking up, etc.

For passing interactable info to UI you can reference the Interactable directly, or subscribe to static Actions on the Interactor.

Note that this is system is meant for beginning high-level complex interactions, and won't always be appropriate for handling very detailed interaction (like in some minigames).