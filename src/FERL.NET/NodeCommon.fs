module NodeCommon

open System.Net
open System.Net.Sockets

type Pid = 
    { Node : string
      Id : int
      Serial : int
      Creation : int }

type LocalNode =
    { NodeName : string
      TcpListener : TcpListener
      Pid : Pid
      Port : int
      Cookie : string}

let CreatePid nodeName port = {Node = nodeName; Id = port; Serial = port; Creation = port}

let IPAddress = Dns.GetHostEntry("localhost").AddressList.[0]

let HostName = Dns.GetHostName()

let BuildNodeName (nodeName:string) = 
    match nodeName.IndexOf("@") with
    | index when index < 0 -> nodeName.ToString() + "@" + HostName
    | _ -> nodeName
