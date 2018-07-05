<template>
<div>
  <br>
  <br>
  <div style="text-align: center;margin-top 2%;font-family: 'Courier New', sans-serif;" class="container">
    <div>
      <h1 style="font-variant: small-caps; font-size: 45px;"> <strong> Mes statistiques </strong></h1>
    </div>
  </div>
  <br><br>
<table>
  <tr>
    <th>Jeu</th>
    <th>Victoires</th>
    <th>Défaites</th>
    <th>Victoire %</th>
    <th>Nombre de parties</th>
  </tr>
  <tr>
    <td>BlackJack</td>
    <td>{{playerwinsbj}}</td>
    <td>{{playerlossesbj}}</td>
    <td>{{playerratiobj}}</td>
    <td>{{playernbgamesbj}}</td>
  </tr>
  <tr>
    <td>Yams</td>
    <td>{{playerwinsy}}</td>
    <td>{{playerlossesy}}</td>
    <td>{{playerratioy}}</td>
    <td>{{playernbgamesy}}</td>
  </tr>
</table>
 <br>
 <br>
 <br>
 <br>
 <br>
<center>

<div class="piecontainer">
<canvas id="pie-chart" class="chartjs" width="770px" height="385px" ></canvas>
</div>

<div class="piecontainer">
<canvas id="pie-chart2" class="chartjs" width="770px" height="385px" ></canvas>
</div>
</center>
</div>
</template>

<script>

import { mapActions } from 'vuex';
import Vue from 'vue';
import GameApiService from '../services/GameApiService';
import Chart from 'chart.js';

export default {
      data() {
        return {
            playerwinsbj: 0,
            playerlossesbj:0,
            playerratiobj: 0,
            playernbgamesbj: 0,
            playerwinsy: 0,
            playerlossesy:0,
            playerratioy: 0,
            playerratioynum: 0,
            playerratiobjnum: 0,
            playernbgamesy: 0
        }
    },

  async mounted() {
    await this.refreshBlackJackstats();
    await this.refreshYamsstats();

new Chart(document.getElementById("pie-chart"), {
    type: 'pie',
    data: {
      labels: ["Victoires", "Defaites"],
      datasets: [{
        label: "Ratio (percentage)",
        backgroundColor: ["#c3c3d5", "#343a40"],
        data: [this.playerratioynum.toFixed(2), (100 - this.playerratioynum).toFixed(2)]
      }]
    },
    options: {
      title: {
        display: true,
        text: 'Ratio (%) for Yams'
      }
    }
});

new Chart(document.getElementById("pie-chart2"), {
    type: 'pie',
    data: {
      labels: ["Victoires", "Defaites"],
      datasets: [{
        label: "Ratio (percentage)",
        backgroundColor: ["#c3c3d5", "#343a40"],
        data: [this.playerratiobjnum.toFixed(2),(100 - this.playerratiobjnum).toFixed(2)]
      }]
    },
    options: {
      title: {
        display: true,
        text: 'Ratio (%) for BlackJack'
      }
    }
});

  },

    
  methods: {
    ...mapActions(['executeAsyncRequest']),

    async refreshBlackJackstats() {
      this.playerwinsbj = await this.executeAsyncRequest(() => GameApiService.getWinsBlackJackPlayer());
      this.playerlossesbj = await this.executeAsyncRequest(() => GameApiService.getLossesBlackJackPlayer());
      this.playernbgamesbj = this.playerwinsbj + this.playerlossesbj;
      this.playerratiobj = this.playerwinsbj / (this.playerwinsbj + this.playerlossesbj);
      this.playerratiobjnum = this.playerratiobj.toFixed(3) * 100;
      this.playerratiobj = (this.playerratiobj.toFixed(3) * 100).toFixed(2) + ' %';
      if(this.playerratiobj == "NaN %") {
        this.playerratiobj = "0 %";
      }
    },

        async refreshYamsstats() {
      this.playerwinsy = await this.executeAsyncRequest(() => GameApiService.getWinsYamsPlayer());
      this.playerlossesy = await this.executeAsyncRequest(() => GameApiService.getLossesYamsPlayer());
      this.playernbgamesy = this.playerwinsy + this.playerlossesy;
      this.playerratioy = this.playerwinsy / (this.playerwinsy + this.playerlossesy);
      this.playerratioynum = this.playerratioy.toFixed(3) * 100;
      this.playerratioy = (this.playerratioy.toFixed(3) * 100).toFixed(2) + ' %';
      if(this.playerratioy == "NaN %") {
        this.playerratioy = "0 %";
      }
    }
  }
  
}
</script>


<style lang="scss">

// #pie-chart {
//    width: 10px;
//    height: 10px;
// }
.piecontainer {
height: 800px;
width: 800px;
display: inline-block;
}

table {
    font-family: arial, sans-serif;
    border-collapse: collapse;
    border-spacing: 0;
    width: 100%;
    border: 1px solid #ddd;
}

tr { 
  background-color: #f2f2f2;
  }

td, th {
    border-bottom: 1px solid #dddddd;
    text-align: left;
    padding: 14px;
    font-family: 'Courier New', sans-serif;
    font-size: 21px;
    font-variant: small-caps;
    font-weight: bold;
}

th {
    background-color: #343a40;
    color: white;
}
</style>