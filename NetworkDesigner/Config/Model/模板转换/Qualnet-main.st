#*************general.config*****
VERSION 6.1
EXPERIMENT-NAME $Scenario.SimName$
EXPERIMENT-COMMENT NONE
SIMULATION-TIME $Scenario.SimTime$S
SEED $Scenario.SimSeed$

#*************Terrain parameter**
COORDINATE-SYSTEM CARTESIAN
TERRAIN-DIMENSIONS ($Scenario.SimRange.Width$, $Scenario.SimRange.Height$)

#*************default parameter**
INCLUDE Qualnet-common

#*************topo parameter*****
$Scenario.AllLink:{LINK N$it.nNet$-$it.net$ {$it.SrcNode.ID$,$it.DestNode.ID$}
[N$it.nNet$-$it.net$] LINK-PHY-TYPE $it.linkType$
[N$it.nNet$-$it.net$] LINK-BANDWIDTH $it.bandwidth$
[N$it.nNet$-$it.net$] FIXED-COMMS-DROP-PROBABILITY $it.dropProbability$
[N$it.nNet$-$it.net$] DUMMY-GUI-SYMMETRIC-LINK YES
[N$it.nNet$-$it.net$] ROUTING-PROTOCOL $it.routeType$
}$

$Scenario.AllNode:{[$it.ID$] HOSTNAME $it.name$
[$it.ID$] NODE-PLACEMENT FILE
}$
NODE-POSITION-FILE $Scenario.SimName$.nodes

#*************traffic parameter*********
$Scenario.AllTraffic:{$it.trafficName$ $it.SrcNode.ID$ $it.DestNode.ID$ $it.trafficParam$
}$

#*************statistics parameter*********
PHY-LAYER-STATISTICS NO
MAC-LAYER-STATISTICS YES
ARP-STATISTICS NO
ROUTING-STATISTICS YES
QUEUE-STATISTICS YES
ICMP-STATISTICS NO
TCP-STATISTICS YES
UDP-STATISTICS YES
APPLICATION-STATISTICS YES