using Godot;
using System;
using System.Collections.Generic;

public class Arena : Node {
  bool singlePlayer;
  List<Actor> actors;
  Spatial terrain;
  
  public void Init(bool singlePlayer){
    this.singlePlayer = singlePlayer;
    actors = new List<Actor>();
    InitTerrain();
    SpawnPlayer(new Vector3(0, 5, 0));
  }
  
  public void InitTerrain(){
    PackedScene ps = (PackedScene)GD.Load("res://Scenes/Prefabs/Terrain.tscn");
    Node instance = ps.Instance();
    AddChild(instance);
    terrain = (Spatial)instance;
  }
  
  public Actor SpawnPlayer(Vector3 pos){
    Actor actor = Actor.ActorFactory(Actor.Brains.Player1);
    actors.Add(actor);
    actor.SetPos(pos);
    AddChild((Node)actor);
    
    return actor;
  }
  
  /* A factory to do all that node stuff in lieu of a constructor */ 
  public static Arena ArenaFactory(){
    PackedScene ps = (PackedScene)GD.Load("res://Scenes/Arena.tscn");
    Node instance = ps.Instance();
    return (Arena)instance;
  }

}