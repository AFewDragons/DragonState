# DragonState
A global state system to use in unity.

[![Discord](https://img.shields.io/discord/686737735356252191.svg)](https://discord.gg/M7Gv6ER)
[![GitHub issues](https://img.shields.io/github/issues/AFewDragons/GlobalState.svg)](https://github.com/AFewDragons/DragonState/issues)
[![GitHub Wiki](https://img.shields.io/badge/wiki-available-brightgreen.svg)](https://github.com/AFewDragons/DragonState/wiki)

### DragonState

Want to make sure the state of your game always stays up to date.

DragonState allows you to use a ready made system that helps you manage the state of your game. Store everything in your game in one place.  
Health, energy, is door open, quests, time clown laughed; anything you want to store.

### Features
* A key/value based state
* Access via Scriptable Objects in the project hierachy
* Create your own state types
* Base states for int/float/string/bool included
* Manipulate state in editor window for testing

### Install

#### Unity Package Manager

To install this project using the Unity Package Manager,
add the following via the Package 

```
https://github.com/AFewDragons/DragonState.git
```

You will need to have Git installed and available in your system's PATH.

#### Asset Folder

Simply copy/paste the source files into your assets folder.

### How To Use

#### Custom State

```
using AFewDragons;

[System.Serializable]
public class Npc
{
	public string Name;
	public int Age;
	public string FavouriteMeal;
}

[CreateAssetMent(fileName = "NpcState", menuName = "States/Npc")]
public class NpcState : DragonStateBase<Npc>
{
	//You can override the get and set methods to change how your state is serialized.
}
```

### Community

Create issues for bugs or feature requests.