module SelfNode

open System.Net
open System.Net.Sockets

type Pid = 
    { Node : string
      Id : int
      Serial : int
      Creation : int }

type LocalNode =
    { TcpListener : TcpListener
      Pid : Pid
      Port : int}

let CreatePid nodeName port = {Node = nodeName; Id = port; Serial = port; Creation = port}

let BuildSelfNode nodeName erlangCookie port =
    let tcpListener = new TcpListener(Dns.GetHostEntry("localhost").AddressList.[0], port)
    do tcpListener.Start()
    let assignedPort = match port with
                       | _ when port <> 0 -> port
                       | _ -> (tcpListener.LocalEndpoint :?> IPEndPoint).Port
    let pid = CreatePid nodeName assignedPort
    {TcpListener = tcpListener; Pid = pid; Port = assignedPort}
   
