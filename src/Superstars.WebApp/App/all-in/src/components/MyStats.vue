<template>

<div>
  <div style="text-align: center;margin-top 2%;font-family: 'Courier New', sans-serif;">
      <h1 style="text-decoration: underline;"> <strong> Mes statistiques : </strong></h1>
  </div>
<table>
  <tr>
    <th>Jeu</th>
    <th>Victoires</th>
    <th>Défaites</th>
    <th>Victoire/Défaite</th>
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

<center>

<div class="piecontainer">
<canvas id="pie-chart" class="chartjs" width="770px" height="385px" ></canvas>
</div>

<div class="piecontainer">
<canvas id="pie-chart2" class="chartjs" width="770px" height="385px" ></canvas>
</div>

</center>
<!-- <canvas id="pie-chart" height="20px" width="40px"></canvas> -->

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
        backgroundColor: ["#3e95cd", "#8e5ea2"],
        data: [this.playerwinsy,this.playerlossesy]
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
        backgroundColor: ["#3e95cd", "#8e5ea2"],
        data: [this.playerwinsbj,this.playerlossesbj]
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
      this.playerratiobj = this.playerratiobj.toFixed(4) * 100 + ' %';
      if(this.playerratiobj == "NaN %") {
        this.playerratiobj = "0 %";
      }
    },

        async refreshYamsstats() {
      this.playerwinsy = await this.executeAsyncRequest(() => GameApiService.getWinsYamsPlayer());
      this.playerlossesy = await this.executeAsyncRequest(() => GameApiService.getLossesYamsPlayer());
      this.playernbgamesy = this.playerwinsy + this.playerlossesy;
      this.playerratioy = this.playerwinsy / (this.playerwinsy + this.playerlossesy);
      this.playerratioy = this.playerratioy.toFixed(4) * 100 + ' %';
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
height: 600px;
width: 600px;
display: inline-block;
}

table {
    font-family: arial, sans-serif;
    border-collapse: collapse;
    border-spacing: 0;
    width: 100%;
    border: 1px solid #ddd;
}

tr:nth-child(even) { 
  background-color: #f2f2f2;
  }

td, th {
    border: 1px solid #dddddd;
    text-align: left;
    padding: 8px;
}
</style>