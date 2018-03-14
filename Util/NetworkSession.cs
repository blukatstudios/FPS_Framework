/*
    The NetworSession is like the Session's little brother that tags along for multiplayer games.
    The prime purpose of this class is to abstract away networked info and functionality that
    the Session may not need during singlePlayer games.
*/

using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;


public class NetworkSession : Node {
  public NetworkedMultiplayerENet peer;
  public const int DefaultPort = 27182;
  private const int MaxPlayers = 10;
  public const string DefaultServerAddress = "127.0.0.1";
  public int selfPeerId;
  public Dictionary<int, List<string>> playerInfo;
  
  public override void _Ready(){
    playerInfo = new Dictionary<int, List<string>>();
  }
  
  public void InitServer(Godot.Object obj, string playerJoin, string playerLeave){
    peer = new Godot.NetworkedMultiplayerENet();
    this.GetTree().Connect("network_peer_connected", obj, playerJoin);
    this.GetTree().Connect("network_peer_disconnected", obj, playerLeave);
    peer.CreateServer(DefaultPort, MaxPlayers);
    this.GetTree().SetNetworkPeer(peer);
    selfPeerId = this.GetTree().GetNetworkUniqueId();
  }
  
  public void InitClient(string address, Godot.Object obj, string success, string fail){
    GetTree().Connect("connected_to_server", obj, success);
    GetTree().Connect("connection_failed", obj, fail);
    peer = new Godot.NetworkedMultiplayerENet();
    peer.CreateClient(address, DefaultPort);
    this.GetTree().SetNetworkPeer(peer);
    selfPeerId = this.GetTree().GetNetworkUniqueId();
  }
  
  
}