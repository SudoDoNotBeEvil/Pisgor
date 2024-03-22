### DialogueSystem scripts:

##### LUA functions:
	PlayerHasItem("Item_Bucket") // bool
	DestroyItem("Item_Crowbar")
	CanPickup() // bool
	TryGiveItem("Item_Mushroom") // bool

##### Conditions:
	CurrentQuestState("LuaTest") == "unassigned"
	CurrentQuestState("LuaTest") == "success"
	PlayerHasItem("Item_Bucket"))

##### Script:
	SetQuestState("LuaTest","greeted")
	SetQuestState("LuaTest","success")
	DestroyItem("Item_Bucket")
	TryGiveItem("Item_Bucket")
