<template>
<div class="page">
  
  <h3 style="letter-spacing: 2px; font-family: 'Courier New', sans-serif; margin-top:2%; margin-left:90%;">TOUR:{{nbTurn}}</h3>
  
  <form @submit="onSubmitAI($event)" id="PlayAI">
    <div v-for="(i, index) of iadices" :key="index" class="iadices">
      <img v-if="i == 1" src="../img/diceia1.png">
      <img v-if="i == 2" src="../img/diceia2.png">
      <img v-if="i == 3" src="../img/diceia3.png">
      <img v-if="i == 4" src="../img/diceia4.png">
      <img v-if="i == 5" src="../img/diceia5.png">
      <img v-if="i == 6" src="../img/diceia6.png">
    </div>
  </form>
  
  <br><br><br><br>
  
  <form @submit="onSubmit($event)" id="PlayPlayer">
    <div v-for="(i, index) of dices" :key="index" class="playerdices">
      <input type="checkbox" :id="index+1" :value="index+1" v-model="selected" v-if="nbTurn != 0 && nbTurn < 3">
      <label class="image-checkbox" :for="index+1">
        <img v-if="i == 1" src="../img/dice1.png">
        <img v-if="i == 2" src="../img/dice2.png">
        <img v-if="i == 3" src="../img/dice3.png">
        <img v-if="i == 4" src="../img/dice4.png">
        <img v-if="i == 5" src="../img/dice5.png">
        <img v-if="i == 6" src="../img/dice6.png">
      </label>
    </div>
  </form>

  <br>
  <div style="text-align:center; letter-spacing: 2px; font-family: 'Courier New', sans-serif;">
    <div v-if="nbTurn != 0 && nbTurn < 3">ClIQUER SUR LES DÉS À RELANCER</div>
    <div v-if="nbTurn == 3 && nbTurnIa == 0">C'EST MAINTENANT AU TOUR DE L'IA</div>
    <div v-if="nbTurnIa == 1">L'IA FAIT SON 1<sup>er</sup> LANCÉ <div class="loader"></div></div>  
    <div v-if="nbTurnIa == 2">L'IA FAIT SON 2<sup>ème</sup> LANCÉ  <div class="loader"></div></div>
    <div v-if="nbTurnIa == 3 && winOrLose == ''">L'IA FAIT SON DERNIER LANCÉ <div class="loader"></div></div>
    <div v-if="nbTurnIa == 3 && winOrLose != ''">L'IA À FINI DE JOUER</div>
    <br>

    <button form="PlayPlayer" type="submit" class="btn btn-light" v-if="nbTurn == 0 ">LANCER</button>
    <button form="PlayPlayer" type="submit" class="btn btn-light" v-if="nbTurn < 3 && nbTurn != 0">RELANCER</button>
    <button form="PlayAI" type="submit" class="btn btn-light" v-if="nbTurn >= 3 && nbTurnIa <1">LANCER L'IA</button>
    <div style="text-transform: capitalize;" v-if="nbTurnIa == 3 && winOrLose != ''">{{winOrLose}}<br>VOTRE FIGURE: <strong>{{playerFigure}}</strong><br>FIGURE DE L'IA: <strong>{{IaFigure}}</strong></div>
  </div>

  <div style="text-align:center;" v-if="nbTurnIa == 3 && winOrLose != ''">
    <br>
    <router-link v-on:click.native="RePlay()" to="">
      <button class="btn btn-light">REJOUER</button>
    </router-link>
    <router-link to="/play">
      <button class="btn btn-dark">QUITTER</button>
    </router-link>
  </div>

</div>
</template>

<script>
import { mapActions } from 'vuex';
import YamsApiService from '../services/YamsApiService';
import GameApiService from '../services/GameApiService';
import WalletApiService from '../services/WalletApiService';
import Vue from 'vue';

export default {
  data(){
    return {
      selected: [],
      dices: [],
      iadices: [],
      nbTurn: 0,
      nbTurnIa: 0,
      winOrLose: '',
      playerFigure: '',
      IaFigure: '',
      wait: '',
      playerwin: false,
    }
  },


  async mounted() {
    await this.refreshDices();
    await this.refreshIaDices();
    var data = await this.executeAsyncRequest(() => WalletApiService.GetFakeBalance());
    GameApiService.Bet(data.balance);
  },
  
  methods: {
    ...mapActions(['executeAsyncRequest']),

    async refreshDices() {
      this.dices = await this.executeAsyncRequest(() => YamsApiService.GetPlayerDices());
    },
    
    async refreshIaDices() {
      this.iadices = await this.executeAsyncRequest(() => YamsApiService.GetIaDices());
    },

    async changeTurn() {
      this.nbTurn = await this.executeAsyncRequest(() => YamsApiService.GetTurn());
    },

    async updateStats() {
        await this.executeAsyncRequest(() => GameApiService.UpdateStats('Yams',this.playerwin));
    },

    async getFinalResult() {
      let tableResult = await this.executeAsyncRequest(() => YamsApiService.GetFinalResult());
      this.IaFigure = tableResult[0];
      this.playerFigure = tableResult[1];
      this.winOrLose = tableResult[2];
      var pot = await this.executeAsyncRequest(() => GameApiService.getYamsPot());
      if(this.winOrLose == "You Lose"){
          await this.updateStats();
      }
      else if(this.winOrLose == "You Win"){
        this.playerwin = true;
        await this.updateStats();
        await this.executeAsyncRequest(() => WalletApiService.WithdrawInBankRoll(pot));
        await this.executeAsyncRequest(() => WalletApiService.CreditPlayer(pot));
      }
    },

    async RePlay() {
      await this.executeAsyncRequest(() => YamsApiService.DeleteYamsAiPlayer());
      await this.executeAsyncRequest(() => GameApiService.DeleteAis());
      await this.executeAsyncRequest(() => GameApiService.createGame('Yams'));
      await this.executeAsyncRequest(() => GameApiService.createAiUser());
      await this.executeAsyncRequest(() => YamsApiService.CreateYamsPlayer());
      await this.executeAsyncRequest(() => YamsApiService.CreateYamsAiPlayer());
      this.$router.go(this.$router.history);
    },

    waiting() {
      if(this.wait == '...'){
        this.wait = '';
      }else{
        this.wait += '.';
      }
    },

    async onSubmitAI(e) {
      e.preventDefault();
      while(this.nbTurnIa < 3) {
        setTimeout(this.waiting, 400);
        setTimeout(this.waiting, 800);
        setTimeout(this.waiting, 1200);
        setTimeout(this.waiting, 1600);
        setTimeout(this.waiting, 2000);
        setTimeout(this.waiting, 2400);
        setTimeout(this.waiting, 2800);
        setTimeout(this.waiting, 3200);      
        let arraydice = [this.iadices, this.dices];
        while(this.wait != '') {
          //Wait end of dynamic '...' for roll dices
          setTimeout(this.waiting, 400);
        }
        this.nbTurnIa = this.nbTurnIa + 1;
        await this.executeAsyncRequest(() => YamsApiService.RollIaDices(arraydice));
        await this.refreshIaDices();
      }
      if(this.nbTurnIa === 3)
        await this.getFinalResult();
    },

    async onSubmit(e) {
      e.preventDefault();
      if(this.nbTurn === 0)
        await this.executeAsyncRequest(() => YamsApiService.RollDices([1,2,3,4,5]));
      else
        await this.executeAsyncRequest(() => YamsApiService.RollDices(this.selected));
      await this.refreshDices();
      this.selected = [];
      if(this.nbTurn < 3)
        await this.changeTurn();
    }
  }
}
</script>

<style>

.loader {
  border: 2px solid #f3f3f3;
  border-radius: 0%;
  border-top: 3px solid 	rgb(160,160,160);
  border-right: 3px solid 	rgb(128,128,128);
  border-bottom: 3px solid 	rgb(190,190,190);
  border-left: 3px solid 	rgb(96,96,96);
  margin: auto;
  width: 8px;
  height: 8px;
  -webkit-animation: spin 1s linear infinite;
  animation: spin 1s linear infinite;
}

@-webkit-keyframes spin {
  0% { -webkit-transform: rotate(0deg); }
  100% { -webkit-transform: rotate(360deg); }
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}

.image-checkbox > img {
	cursor: pointer;
  height: 100px;
  width: 100px;
}

input[type="checkbox"] {
	display: none;
}

input[type=checkbox]:checked + label > img{
  height: 120px;
  width: 120px;
} 

.playerdices {
	display: inline-block;
	position: relative;
  width: 8%; 
	left: 30.2%;
}

.iadices {
	display: inline-block;
	position: relative;
  width: 8%;
	left: 30.2%;
  margin-top: 8%;
}

.iadices > img {
  height: 100px;
  width: 100px;  
}
</style>


  