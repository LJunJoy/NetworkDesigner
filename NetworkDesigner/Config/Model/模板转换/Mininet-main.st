#!/usr/bin/python
from mininet.topo import Topo
from mininet.cli import CLI
from mininet.net import Mininet

class MDASimTopo( Topo ):
    "MDA Generate topology. $Scenario.SimName$"
    def __init__( self ):
        Topo.__init__( self )
        # Add hosts
$Scenario.AllHosts:{
        $it.name$ = self.addHost( '$it.name$' )}$
		# Add switches
$Scenario.AllSwitchs:{
        $it.name$ = self.addSwitch( '$it.name$' )}$
$Scenario.AllRoutes:{
        $it.name$ = self.addSwitch( '$it.name$' )}$
        # Add links
$Scenario.AllLink:{
        self.addLink( $it.SrcNode.name$, $it.DestNode.name$ )}$
topos = { 'SimulationTopo': ( lambda: MDASimTopo() ) }
def runSimuTopo():
    topo = MDASimTopo()
    net = Mininet( topo=topo )
    net.start()
    CLI( net )
    net.stop()
if __name__ == '__main__':
    runSimuTopo()
	