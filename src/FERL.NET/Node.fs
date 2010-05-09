module Node

open System.Net
open System.Net.Sockets
open NodeCommon

let EpmdPort = 4369

let BuildPeerNode nodeName =
    let assignedNodeName = BuildNodeName nodeName
    let pid = CreatePid assignedNodeName 0
    {NodeName = assignedNodeName; TcpListener = null; Pid = pid; Port = 0; Cookie = null}

let BuildSelfNode nodeName erlangCookie port =
    let tcpListener = new TcpListener(IPAddress, port)
    do tcpListener.Start()
    let assignedPort = match port with
                       | _ when port <> 0 -> port
                       | _ -> (tcpListener.LocalEndpoint :?> IPEndPoint).Port
    let assignedNodeName = BuildNodeName nodeName
    let pid = CreatePid assignedNodeName assignedPort
    {NodeName = assignedNodeName; TcpListener = tcpListener; Pid = pid; Port = assignedPort; Cookie = erlangCookie}

let GetNodeNameParts (nodeName:string) =
    let nodeNameParts = nodeName.Split('@')
    nodeNameParts.[0], nodeNameParts.[1]

let LookupPortInEmpd node =
    let nodeShortName, nodeHostName = GetNodeNameParts node.NodeName
    let epmdTcpClient = new TcpClient(nodeHostName, EpmdPort)
    let epmdResponseStream = epmdTcpClient.GetStream()
    //epmdResponseStream.ReadByte()
//				int response = ibuf.read1();
//				if (response == port4resp)
//				{
//					int result = ibuf.read1();
//					if (result == 0)
//					{
//						port = ibuf.read2BE();
//						
//						node.ntype = ibuf.read1();
//						node._proto = ibuf.read1();
//						node._distHigh = ibuf.read2BE();
//						node._distLow = ibuf.read2BE();
//						// ignore rest of fields
//					}
//				}
    0


let ConnectNodes selfNode peerNode = 
    {SelfNode = selfNode; PeerNode = peerNode; IsConnected = true}