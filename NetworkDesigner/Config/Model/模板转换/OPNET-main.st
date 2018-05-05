<?xml version="1.0"?>
<!DOCTYPE network SYSTEM "network.dtd">
<network locale="C" version="1.6" attribute_processing="explicit">
  <attr name="x position" value="0.0"/>
  <attr name="y position" value="0.0"/>
  <attr name="x span" value="360"/>
  <attr name="y span" value="180"/>
  <attr name="creation source" value="Object Palette"/>
  <attr name="creation timestamp" value="Unknown"/>
  <attr name="creation data" value=""/>
  <characteristic name="units" value="Degrees"/>
  <view-props>
  	<attr name="map" value="world"/>
  	<attr name="slice.count" value="0"/>
  	<attr name="grid division" value="15"/>
  	<attr name="grid style" value="dashed"/>
  	<attr name="grid color" value="#808080"/>
  	<attr name="resolution" value="2.5"/>
  	<attr name="ul x" value="-180"/>
  	<attr name="ul y" value="90"/>
  	<attr name="lr x" value="109.6"/>
  	<attr name="lr y" value="-90"/>
  	<attr name="iso map names.count" value="0"/>
  	<attr name="current view" value="Default View"/>
  	<attr name="view mode" value="geographic"/>
  </view-props>
  <subnet name="Office Network">
    <attr name="priority" value="0"/>
  	<attr name="user id" value="0"/>
  	<attr name="x position" value="0.0"/>
  	<attr name="y position" value="0.0"/>
  	<attr name="x span" value="0.00898315295161"/>
  	<attr name="y span" value="0.0089831528412"/>
  	<attr name="outline color" value="blue"/>
  	<attr name="creation source" value="Object Palette"/>
  	<attr name="creation timestamp" value="11:30:22 Apr 16 2018"/>
  	<attr name="creation data" value=""/>
  	<characteristic name="units" value="Meters"/>
  	<view-props>
  	  <attr name="map" value="NONE"/>
  	  <attr name="slice.count" value="0"/>
  	  <attr name="grid division" value="125"/>
  	  <attr name="grid style" value="dashed"/>
  	  <attr name="grid color" value="#808080"/>
  	  <attr name="resolution" value="0.522002"/>
  	  <attr name="ul x" value="0"/>
  	  <attr name="ul y" value="0"/>
  	  <attr name="lr x" value="1000"/>
  	  <attr name="lr y" value="990.418"/>
  	  <attr name="iso map names.count" value="0"/>
  	  <attr name="current view" value="Default View"/>
  	  <attr name="view mode" value="geographic"/>
  	</view-props>
$Scenario.AllHosts:{
	<node name="$it.name$" model="sim_mda_wkstn" ignore_questions="true">
	  <attr name="x position" value="$it.X$"/>
	  <attr name="y position" value="$it.Y$"/>
	  <attr name="icon name" value="station"/>
	</node>
}$
$Scenario.AllSwitchs:{
	<node name="$it.name$" model="sim_mda_switch" ignore_questions="true">
	  <attr name="x position" value="$it.X$"/>
	  <attr name="y position" value="$it.Y$"/>
	  <attr name="icon name" value="sw_3com"/>
	</node>
}$
$Scenario.AllRoutes:{
	<node name="$it.name$" model="sim_mda_router" ignore_questions="true">
	  <attr name="x position" value="$it.X$"/>
	  <attr name="y position" value="$it.Y$"/>
	  <attr name="icon name" value="rtr_3com"/>
	</node>
}$
$Scenario.AllLink:{
	<link name="$it.SrcNode.name$ &lt;-&gt; $it.DestNode.name$" model="100BaseT" class="duplex" srcNode="$it.SrcNode.name$" destNode="$it.DestNode.name$" ignore_questions="true">
	  <attr name="transmitter a" value="$it.SrcNode.name$.port_tx_$it.SrcIF$"/>
	  <attr name="receiver a" value="$it.SrcNode.name$.port_rx_$it.SrcIF$"/>
	  <attr name="transmitter b" value="$it.DestNode.name$.port_tx_$it.DestIF$"/>
	  <attr name="receiver b" value="$it.DestNode.name$.port_rx_$it.DestIF$"/>
	  <attr name="arrowheads" value="head and tail"/>
	  <attr name="color" value="#850000"/>
	  <attr name="line style" value="solid"/>
	  <attr name="symbol" value="none"/>
	  <attr name="thickness" value="1"/>
	</link>
}$
  </subnet>
</network>