module SelfNode

open System.Net
open System.Net.Sockets
open NodeCommon

let BuildSelfNode nodeName erlangCookie port =
    let tcpListener = new TcpListener(IPAddress, port)
    do tcpListener.Start()
    let assignedPort = match port with
                       | _ when port <> 0 -> port
                       | _ -> (tcpListener.LocalEndpoint :?> IPEndPoint).Port
    let assignedNodeName = BuildNodeName nodeName
    let pid = CreatePid assignedNodeName assignedPort
    {NodeName = assignedNodeName; TcpListener = tcpListener; Pid = pid; Port = assignedPort; Cookie = erlangCookie}
   
