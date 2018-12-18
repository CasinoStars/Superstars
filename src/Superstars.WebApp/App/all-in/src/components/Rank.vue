<template>
  <div class="rank">
    
    <div style="text-align: center; font-family: 'Courier New', sans-serif;">
      <h1 style="margin-top: 3%; font-variant: small-caps; font-size: 45px;">
        <i class="fa fa-chevron-left" @click="SwapTrueOrFake()" id="chevron"></i>
        <strong v-if="TrueOrFake">Classement En Bits</strong>
        <strong v-else>Classement En FakeCoins</strong>
        <i class="fa fa-chevron-right" @click="SwapTrueOrFake()" id="chevron"></i>
      </h1>
    </div>
    <div style="margin-left: 50%;">
      <div v-if="TrueOrFake" id="blueCircle"><div v-if="TrueOrFake" style="margin-top: -2px; margin-left:10px;" id="whiteCircle"></div></div>
      <div v-if="!TrueOrFake" id="whiteCircle"><div v-if="!TrueOrFake" style="margin-top: -2px; margin-left:10px;" id="blueCircle"></div></div>
    </div>

    <table style="margin-top:3%;">
      <thead>
        <tr>
          <th>Rang</th>
          <th>Pseudo</th>
          <th>Profit</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(e, index) of playersProfitData" :key='index'>
          <td>{{index+1}}</td>
          <td><a class="link" :href="'statistics?pseudo='+e.userName">{{e.userName}}</a></td>
          <td>{{e.profit.toLocaleString('en')}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>

import { mapActions } from 'vuex';
import Vue from 'vue';
import RankApiService from '../services/RankApiService';


export default {
  data() {
    return {
      TrueOrFake: true,
      playersProfitData: []
    }
  },    

  async mounted() {
    await this.getPlayersProfit();
  },

  methods: {
    ...mapActions(['executeAsyncRequest']),

    SwapTrueOrFake(){
      this.TrueOrFake = !this.TrueOrFake;
      this.changethetab();
    },

    async getPlayersProfit(){
      if(this.TrueOrFake)
      this.playersProfitData = await this.executeAsyncRequest(() => RankApiService.GetPlayersGlobalProfit(1));
      else
      this.playersProfitData = await this.executeAsyncRequest(() => RankApiService.GetPlayersGlobalProfit(0));
    },

    async changethetab(){
      await this.getPlayersProfit();
    }  
  }
}
</script>

<style lang="scss">
 .rank .nav-link:hover {
    opacity: 0.5;
 }
 .rank #chevron {
    color: gray;
    font-size: 50%;
 }
 .rank #chevron:hover {
    cursor: pointer;
    opacity: 0.5;
 }
 .rank #blueCircle {
    background:#f1f3f3;
    border-radius:40%;
    width:10px;
    height:10px;
    border:2px solid #0e97d7;
  }
  .rank #whiteCircle {
    background:#f1f3f3;
    border-radius:40%;
    width:10px;
    height:10px;
    border:2px solid #81888b;
  }
  
  .rank table {
    font-family: arial, sans-serif;
    border-collapse: collapse;
    border-spacing: 0;
    width: 100%;
    border: 1px solid #ddd;
  }

  .rank th {
    background-color: #343a40;
    color: white;
  }

  .rank tr { 
  background-color: #f2f2f2;;
  }

  .rank thead {
    text-align: center;
  }
  
  .rank td,th {
    border-bottom: 1px solid #dddddd;
    text-align: center;
    font-family: 'Courier New', sans-serif;
    font-variant: small-caps;
    font-weight: bold;
  }

  .rank tbody tr:hover {
    background-color: #343a40;
    color:white;
    a{
      color: white;
    }
    a:hover{
      color: grey;
    }
  }

  .rank a{
    color: black;
    font-family: 'Courier New', sans-serif;
    font-size: 120%;
  }
  
</style>