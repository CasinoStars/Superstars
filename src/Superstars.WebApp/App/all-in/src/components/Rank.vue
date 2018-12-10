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
      <tr>
        <th>Pseudo</th>
        <th>Profit/Perte</th>
        <th>Parties de Yams joué</th>
        <th>Parties de BlackJack joué</th>
      </tr>
      <tr>
        <th>
        <div v-for="(e,index) of pseudos" :key='index'>
          <td><a :href="'/statistics?pseudo='+e" style="color: white;"> {{e}} </a></td>
        </div>
        </th>
        <th>
        <div v-for="(e,index) of profits" :key='index'>
          <td>{{e.toLocaleString('en')}}</td>
        </div>
        </th>
        <th>
        <div v-for="(e,index) of playeryamsnbgames" :key='index'>
          <td>{{e}}</td>
        </div>
        </th>
        <th>
        <div v-for="(e,index) of playerblackjacknbgames" :key='index'>
          <td>{{e}}</td>
        </div>
        </th>
      </tr>
    </table>
        
  </div>
</template>

<script>

import { mapActions } from 'vuex';
import Vue from 'vue';
import GameApiService from '../services/GameApiService';
import WalletApiService from '../services/WalletApiService';
import UserApiService from '../services/UserApiService';
import RankApiService from '../services/RankApiService';


export default {
  data() {
    return {
      TrueOrFake: true,
      pseudos: [],
      profits: [],
      playerblackjacknbgames: [],
      playeryamsnbgames: []
    }
  },    

  async mounted() {
    this.pseudos = await this.executeAsyncRequest(() => RankApiService.GetPlayersUserNameSorted(this.TrueOrFake));
    this.profits = await this.executeAsyncRequest(() => RankApiService.GetPlayersProfitSorted(this.TrueOrFake));
    this.playeryamsnbgames = await this.executeAsyncRequest(() => RankApiService.GetPlayersYamsNumberParts(this.TrueOrFake));
    this.playerblackjacknbgames = await this.executeAsyncRequest(() => RankApiService.GetPlayersBlackJackNumberParts(this.TrueOrFake));
  },

  methods: {
    ...mapActions(['executeAsyncRequest']),

    SwapTrueOrFake(){
      this.TrueOrFake = !this.TrueOrFake;
      this.changethetab();
    },

    async changethetab(){
      this.pseudos = await this.executeAsyncRequest(() => RankApiService.GetPlayersUserNameSorted(this.TrueOrFake));
      this.profits = await this.executeAsyncRequest(() => RankApiService.GetPlayersProfitSorted(this.TrueOrFake));
      this.playeryamsnbgames = await this.executeAsyncRequest(() => RankApiService.GetPlayersYamsNumberParts(this.TrueOrFake));
      this.playerblackjacknbgames = await this.executeAsyncRequest(() => RankApiService.GetPlayersBlackJackNumberParts(this.TrueOrFake));
    }  
  }
}
</script>

<style lang="css">
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
</style>