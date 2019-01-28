<template>
  <div class="stats">

    <h1>
      <i class="fa fa-chevron-left" @click="SwapTrueOrFake()" id="chevron"></i>
      <strong v-if="$route.query.pseudo">Statistiques de {{queryPseudo}}</strong>
      <strong v-else>Mes statistiques</strong>
      <i class="fa fa-chevron-right" @click="SwapTrueOrFake()" id="chevron"></i>
    </h1>

    <center>
      <i class="fa fa-btc" id="btc" style="font-size: 1.4rem;" @click="SwapTrueOrFake()"></i>
      <i class="fa fa-money" id="fake" style="font-size: 0.8rem;" @click="SwapTrueOrFake()"></i>
    </center>

    <div class="table-responsive">
      <table>
        <tr>
          <th>Jeu</th>
          <th>Profit</th>
          <th>Mise Total</th>
          <th>Mise Moyenne</th>
          <th>Victoire</th>
          <th>Défaite</th>
          <th>Egalité</th>
          <th>Temps moyen</th>
        </tr>
        <tr v-for="(e,index) of playerStatsData" :key="index">
          <td>{{e.gameName}}</td>
          <td>{{e.profit.toLocaleString('en')}}</td>
          <td>{{e.totalBet.toLocaleString('en')}}</td>
          <td v-if="(e.wins+e.losses+e.equality) == 0">0</td>
          <td v-else>{{(e.totalBet/(e.wins+e.losses+e.equality)).toLocaleString('en')}}</td>
          <td>{{e.wins.toLocaleString('en')}}</td>
          <td>{{e.losses.toLocaleString('en')}}</td>
          <td>{{e.equality.toLocaleString('en')}}</td>
          <td v-if="e.gameName != 'Crash'">{{e.averageTime}}</td><td v-else></td>
        </tr>
      </table>
    </div>

    <div class="container">
      <div class="row">
        <div class="col-sm">
          <canvas id="pie-chart" class="chartjs"></canvas>
        </div>
        <div class="col-sm">
          <canvas id="pie-chart2" class="chartjs"></canvas>
        </div>
        <div class="col-sm">
          <canvas id="pie-chart3" class="chartjs"></canvas>
        </div>
      </div>
    </div>
    
    <router-view :key="$route.fullPath"> </router-view>
  </div>
</template>

<script>
  import {
    mapActions
  } from 'vuex';
  import Vue from 'vue';
  import GameApiService from '../services/GameApiService';
  import UserApiService from '../services/UserApiService';
  import Chart from 'chart.js';
  import RankApiService from '../services/RankApiService';

  export default {
    data() {
      return {
        playerRatioYams: 0,
        playerRatioBj: 0,
        playerRatioCrash: 0,
        queryPseudo: '',
        playerStatsData: [],
        TrueOrFake: true
      }
    },

    async mounted() {
      if (this.$route.query.pseudo)
        this.queryPseudo = this.$route.query.pseudo;
      else
        this.queryPseudo = UserApiService.pseudo;
      await this.getPlayerStats(1);
      this.drawDiagram();
    },

    watch: {
      $route: async function () {
        if (this.$route.query.pseudo)
          this.queryPseudo = this.$route.query.pseudo;
        else
          this.queryPseudo = UserApiService.pseudo;
        await this.getPlayerStats(1);
        this.drawDiagram();
      }
    },

    methods: {
      ...mapActions(['executeAsyncRequest']),

      drawDiagram() {
        new Chart(document.getElementById("pie-chart"), {
          type: 'pie',
          data: {
            labels: ["Victoires", "Defaites"],
            datasets: [{
              label: "Ratio (percentage)",
              backgroundColor: ["#c3c3d5", "#343a40"],
              data: [this.playerRatioYams.toFixed(2), (100 - this.playerRatioYams).toFixed(2)]
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
              data: [this.playerRatioBj.toFixed(2), (100 - this.playerRatioBj).toFixed(2)]
            }]
          },
          options: {
            title: {
              display: true,
              text: 'Ratio (%) for BlackJack'
            }
          }
        });

        new Chart(document.getElementById("pie-chart3"), {
          type: 'pie',
          data: {
            labels: ["Victoires", "Defaites"],
            datasets: [{
              label: "Ratio (percentage)",
              backgroundColor: ["#c3c3d5", "#343a40"],
              data: [this.playerRatioCrash.toFixed(2), (100 - this.playerRatioCrash).toFixed(2)]
            }]
          },
          options: {
            title: {
              display: true,
              text: 'Ratio (%) for Crash'
            }
          }
        });
      },

      async getPlayerStats(moneyTypeId) {
        this.playerStatsData = await this.executeAsyncRequest(() => RankApiService.GetPlayerStats(this.queryPseudo,
          moneyTypeId));

        this.playerStatsData.forEach(element => {
          element.averageTime = this.msToTime(element.averageTime);

          if (element.gameName == 'BlackJack')
            this.playerRatioBj = (element.wins / (element.wins + element.losses)).toFixed(3) * 100;

          if (element.gameName == 'Yams')
            this.playerRatioYams = (element.wins / (element.wins + element.losses)).toFixed(3) * 100;

          if (element.gameName == 'Crash')
            this.playerRatioCrash = (element.wins / (element.wins + element.losses)).toFixed(3) * 100;
        });
      },

      msToTime(element) {
        var milliseconds = parseInt((element % 1000) / 100),
          seconds = parseInt((element / 1000) % 60),
          minutes = parseInt((element / (1000 * 60)) % 60),
          hours = parseInt((element / (1000 * 60 * 60)) % 24);

        hours = (hours < 10) ? "0" + hours : hours;
        minutes = (minutes < 10) ? "0" + minutes : minutes;
        seconds = (seconds < 10) ? "0" + seconds : seconds;

        return hours + ":" + minutes + ":" + seconds + "." + milliseconds;
      },

      async SwapTrueOrFake() {
        this.TrueOrFake = !this.TrueOrFake;
        var iconBc = document.getElementById("btc");
        var iconMoney = document.getElementById("fake");
        if (this.TrueOrFake) {
          await this.getPlayerStats(1);
          iconBc.style.fontSize = "1.4rem";
          iconMoney.style.fontSize = "0.8rem";
        } else {
          await this.getPlayerStats(0);
          iconBc.style.fontSize = "0.8rem";
          iconMoney.style.fontSize = "1.4rem";
        }
        this.drawDiagram();
      }
    }
  }
</script>

<style lang="scss">
  @media(max-width: 562px) {
    .stats h1 {
      margin-top: 3%;
      font-variant: small-caps;
      font-size: 4vh;
      text-align: center;
      font-family: 'Courier New', sans-serif;
    }
  }

  @media(min-width: 562px) {
    .stats h1 {
      margin-top: 3%;
      font-variant: small-caps;
      font-size: 45px;
      text-align: center;
      font-family: 'Courier New', sans-serif;
    }
  }

  .stats {
    overflow-x: hidden;
  }

  .stats i:hover {
    cursor: pointer;
  }

  .stats #chevron {
    color: gray;
    font-size: 50%;
  }

  .stats #chevron:hover {
    opacity: 0.5;
  }

  .stats .container {
    margin-top: 2%;

    .row {
      margin-left: -25%;
      height: 150%;
      width: 150%;
    }
  }

  .stats table {
    font-family: arial, sans-serif;
    border-collapse: collapse;
    border-spacing: 0;
    width: 100%;
    border: 1px solid #ddd;
    margin-top: 3%;
  }

  .stats tr {
    background-color: #f2f2f2;
  }

  .stats td,
  th {
    border-bottom: 1px solid #dddddd;
    text-align: left;
    padding: 14px;
    font-family: 'Courier New', sans-serif;
    font-size: 21px;
    font-variant: small-caps;
    font-weight: bold;
  }

  .stats th {
    background-color: #343a40;
    color: white;
  }
</style>