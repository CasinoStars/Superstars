<template>
<div class="yams">

  <!-- The Modal -->
  <div id="myModal" class="modal">
    <!-- Modal content -->
    <div class="modal-content">

      <div class="modal-header">
        <div style="margin-left: 20%; padding-top: 2px; font-family: 'Courier New', sans-serif;">
          <h2 v-if="realOrFake == 'real'">SOLDE DE VOTRE COMPTE BTC: {{trueCoins}} <i class="fa fa-btc" style="font-size: 1.5rem;"></i></h2>
          <h2 v-else>SOLDE DE VOTRE COMPTE ALL'IN: {{fakeCoins.balance}} <i class="fa fa-money" style="font-size: 1.5rem;"></i></h2>
        </div>
        <router-link class="close"  v-on:click.native="setisingamefalseandredirect()" to="">&times;</router-link>
      </div>
      <ul class="tab-group">
                <li class="tab active" v-if="this.realOrFake == 'real'"><a v-on:click="changeBet('real')">Réel</a></li>
                <li class="tab" v-else><a v-on:click="changeBet('real')">Réel</a></li>
                <li class="tab active" v-if="this.realOrFake == 'fake'"><a v-on:click="changeBet('real')">Virtuel</a></li>
                <li class="tab" v-else><a v-on:click="changeBet('fake')">Virtuel</a></li>
      </ul>
      <form @submit="bet($event)">
        <div  class="modal-body">
          <h4 style="color: white; font-family: 'Courier New', sans-serif;" v-if="realOrFake == 'real'">SAISISSEZ VOTRE MISE EN BTC <span class="req">*</span></h4>
          <h4 style="color: white; font-family: 'Courier New', sans-serif;" v-else>SAISISSEZ VOTRE MISE EN ALL'IN <span class="req">*</span></h4>
          <input style="margin-top: 10px; margin-bottom: 1%;" v-if="realOrFake == 'real'" type="number" v-model="trueBet" required/>
          <input style="margin-top: 10px; margin-bottom: 1%;" v-else type="number" v-model="fakeBet" required/>
        
          <div class="alert alert-danger" style="opacity: 0.8; font-weight: bold; font-family: 'Courier New', sans-serif; text-transform: uppercase; margin-top: 1%; margin-bottom: 0.5%; text-align: center; font-family: 'Courier New', sans-serif;" v-if="errors.length > 0">
            <div style="opacity: 0.7;" v-for="e of errors" :key="e">{{e}}</div>
          </div>
        </div>

        <div class="modal-footer">
          <div style="margin-right: 42%;">
            <router-link class="btn btn-secondary" v-on:click.native="setisingamefalseandredirect()" to="">Annuler</router-link>
            <button type="submit" class="btn btn-light">Confirmer</button>
          </div>
        </div>
      </form>
    </div>
  </div>

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
    <div v-if="nbTurn != 0 && nbTurn < 3">ClIQUER SUR LES DÉS À RELANCER </div>
    <div v-if="nbTurn == 3 && nbTurnIa == 0">C'EST MAINTENANT AU TOUR DE L'IA </div>
    <div v-if="nbTurnIa == 1">L'IA FAIT SON 1<sup>er</sup> LANCÉ  </div>  
    <div v-if="nbTurnIa == 2">L'IA FAIT SON 2<sup>ème</sup> LANCÉ   </div>
    <div v-if="nbTurnIa == 3 && winOrLose == ''">L'IA FAIT SON DERNIER LANCÉ  </div>
    <div v-if="nbTurnIa == 3 && winOrLose != ''">L'IA À FINI DE JOUER</div>
    

<center>
<div v-if="nbTurnIa == 1 || nbTurnIa == 2 || nbTurnIa == 3 && winOrLose == ''" class="lds-css ng-scope">
  <div style="width:100%;height:100%" class="lds-eclipse">
    <div>
      </div>
      </div>
      </div>
</center>

    <!-- <div v-if="nbTurnIa == 1 || nbTurnIa == 2 || nbTurnIa == 3 && winOrLose == ''" class="spinner">
  <div class="cube1"></div>
  <div class="cube2"></div>
    </div> -->

    <button form="PlayPlayer" type="submit" class="btn btn-light" v-if="nbTurn == 0 && playerBet">LANCER</button>
    <button form="PlayPlayer" type="submit" class="btn btn-light" v-if="nbTurn < 3 && nbTurn != 0 && selected != 0">RELANCER</button>
    <button form="PlayPlayer" type="submit" class="btn btn-light" v-if="nbTurn < 3 && nbTurn != 0 && selected == 0" @click="nbTurn = 3">GARDER MES DÉS</button>
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
      playerBet: false,
      realOrFake: 'real',
      profit: 0,
      fakeBet: 0,
      trueBet: 0,
      fakeCoins: 0,
      trueCoins: 0,
      errors: [],
      isingame: 0,
    }
  },

  async mounted() {
    await this.getFakeCoins();
    await this.getTrueCoins();
    await this.refreshDices();
    await this.refreshIaDices();
    await this.getIsingame();
    await this.changeTurn();
    if(this.isingame == 0) {
      this.showModal();
      await this.setisingametrue();
    }

  },
  
  methods: {
    ...mapActions(['executeAsyncRequest']),
    ...mapActions(['executeAsyncRequestWithFakeMoney']),
    ...mapActions(['executeAsyncRequestWithBTCMoney']),
    
    

    async setisingametrue() {
        await this.executeAsyncRequest(() => YamsApiService.SetIsingameyamstrue());
    },

    async setisingamefalse() {
        await this.executeAsyncRequest(() => YamsApiService.SetIsingameyamsfalse());
    },

    async setisingamefalseandredirect() {
        await this.executeAsyncRequest(() => YamsApiService.SetIsingameyamsfalse());
        this.$router.push({ path: 'play' });
    },

    async getIsingame() {
      this.isingame = await this.executeAsyncRequest(() => YamsApiService.Getisingame());
      if(this.isingame == 1) {
        this.playerBet = true;
      }
    },

    async getFakeCoins() {
      this.fakeCoins = await this.executeAsyncRequest(() => WalletApiService.GetFakeBalance());
    },

    async getTrueCoins() {
      this.trueCoins = await this.executeAsyncRequest(() => WalletApiService.GetTrueBalance());
    },


    changeBet(choice){
      this.realOrFake = choice;
      this.errors = 0;
    },

    showModal() {
      var modal = document.getElementById('myModal');
      modal.style.display = "block";
    },

    async bet(e) {
      e.preventDefault();
      var errors = [];
      this.errors = 0;
      if(this.realOrFake === 'fake') {
        if(this.fakeBet > 1000000)
          errors.push("La mise maximum est de 1,000,000");
        else if(this.fakeBet <= 0)  
          errors.push("La mise doit être supérieur à 0");
        else if(this.fakeBet > this.fakeCoins.balance)
          errors.push("Vous n'avez pas cette somme sur votre compte");
      }
      else {
        if(this.trueBet > 10000000)
          errors.push("La mise maximum est de 10,000,000 bits");
        else if(this.trueBet <= 0)  
          errors.push("La mise doit être supérieur à 0 bits");
        else if(this.trueBet > this.trueCoins){
          errors.push("Vous n'avez pas cette somme sur votre compte");}
      }
      this.errors = errors;
      if(errors.length == 0) {
        try {
          if(this.realOrFake === 'fake')
            await this.executeAsyncRequestWithFakeMoney(() => GameApiService.BetFake(this.fakeBet, 'Yams'));
          else
            await this.executeAsyncRequestWithBTCMoney(() => GameApiService.BetBTC(this.trueBet, 'Yams'));
          var modal = document.getElementById('myModal');
          modal.style.display = "none";
          this.playerBet = true;
        }
        catch(error) {
        }
      }
    },

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
        await this.setisingamefalse();
        await this.executeAsyncRequest(() => YamsApiService.DeleteYamsAiPlayer());
        await this.executeAsyncRequest(() => GameApiService.DeleteAis());
    },

    async getFinalResult() {
      let tableResult = await this.executeAsyncRequest(() => YamsApiService.GetFinalResult());
      this.IaFigure = tableResult[0];
      this.playerFigure = tableResult[1];
      this.winOrLose = tableResult[2];
      var pot = await this.executeAsyncRequest(() => GameApiService.getYamsPot());
      if(this.winOrLose == "You Lose") {
          await this.updateStats();
      }
      else if(this.winOrLose == "You Win"){
        this.playerwin = true;
        await this.updateStats();
          if(this.trueBet === 0) {
            await this.executeAsyncRequest(() => WalletApiService.WithdrawFakeBankRoll(pot));
            await this.executeAsyncRequestWithFakeMoney(() => WalletApiService.CreditPlayerInFake(pot));
          }
          else {
            await this.executeAsyncRequest(() => WalletApiService.WithdrawBTCBankRoll(pot));
            await this.executeAsyncRequestWithBTCMoney(() => WalletApiService.CreditPlayerInBTC(pot));
          }
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
        // setTimeout(this.waiting, 400);
        // setTimeout(this.waiting, 800);
        // setTimeout(this.waiting, 1200);
        // setTimeout(this.waiting, 1600);
        // setTimeout(this.waiting, 2000);
        // setTimeout(this.waiting, 2400);
        // setTimeout(this.waiting, 2800);
        // setTimeout(this.waiting, 3200);      
        let arraydice = [this.iadices, this.dices];
        // while(this.wait != '') {
        //   //Wait end of dynamic '...' for roll dices
        //   setTimeout(this.waiting, 400);
        // }
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

<style lang="scss">
$body-bg: #c1bdba;
$form-bg: #13232f;
$white: #ffffff;
$main: #777c7b;
$main-dark: darken($main,5%);
$gray-light: #a0b3b0;

.yams .tab-group {
  list-style:none;
  padding:0;
  margin:0 0 5px 0;
  &:after {
    content: "";
    display: table;
    clear: both;
  }
  li a {
    display:block;
    text-decoration:none;
    padding:8px;
    background:rgba($gray-light,.25);
    color:$gray-light;
    font-size:20px;
    float:left;
    width:50%;
    text-align:center;
    cursor:pointer;
    transition:.5s ease;
    &:hover {
      background:$main-dark;
      color:$white;
    }
  }
  .active a {
    background:$main;
    color:$white;
  }
}

/* The Modal (background) */
.yams .modal {
    display: none; /* Hidden by default */
    position: fixed; /* Stay in place */
    z-index: 1; /* Sit on top */
    padding-top: 16%; /* Location of the box */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgb(0,0,0); /* Fallback color */
    background-color: rgba($body-bg,0.4); /* Black w/ opacity */
}

/* Modal Content */
.yams .modal-content {
    position: relative;
    background: rgba($form-bg,.9);
    margin: auto;
    padding: 0;
    width: 80%;
    box-shadow: 0 0 100px 50px rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
    -webkit-animation-name: animatetop;
    -webkit-animation-duration: 0.4s;
    animation-name: animatetop;
    animation-duration: 0.4s
}

/* Add Animation */
@-webkit-keyframes animatetop {
    from {top:-300px; opacity:0} 
    to {top:0; opacity:1}
}

@keyframes animatetop {
    from {top:-300px; opacity:0}
    to {top:0; opacity:1}
}

/* The Close Button */
.yams .close {
    color: white;
    float: right;
    font-size: 28px;
    font-weight: bold;
}

.yams .close:hover,
.yams .close:focus {
    color: white;
    text-decoration: none;
    cursor: pointer;
}

.req {
	margin:2px;
	color:#1ab188;;
}

.yams .modal-header {
    padding: 10px 16px;
    text-align: center;
    background: #222222a8;
    color: white;
}

.yams .modal-body {
  padding: 20px 16px;
  text-align: center;
}

.yams .modal-footer {
  padding: 15px 16px;
  background-color:  #222222a8;
  color: white;
}
</style>

<style>
/* ECLIPSE LOADER */

@keyframes lds-eclipse {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
  }
  50% {
    -webkit-transform: rotate(180deg);
    transform: rotate(180deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}
@-webkit-keyframes lds-eclipse {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
  }
  50% {
    -webkit-transform: rotate(180deg);
    transform: rotate(180deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}

.yams .lds-eclipse {
  position: relative;
}

.yams .lds-eclipse div {
  position: absolute;
  -webkit-animation: lds-eclipse 1s linear infinite;
  animation: lds-eclipse 1s linear infinite;
  width: 40px;
  height: 40px;
  top: 80px;
  margin-left: -250%;
  border-radius: 50%;
  box-shadow: 0 2px 0 0 #7f8387;
  -webkit-transform-origin: 20px 21px;
  transform-origin: 20px 21px;
}

.yams .lds-eclipse {
  width: 200px !important;
  height: 200px !important;
  -webkit-transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
  transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
}



/* DOUBLE CUBER SPINER */

/* .spinner {
  margin: 100px auto;
  width: 40px;
  height: 40px;
  position: relative;
  display: -webkit-inline-box;
}

.cube1, .cube2 {
  background-color: #333;
  width: 15px;
  height: 15px;
  position: absolute;
  top: 0;
  left: 0;
  
  -webkit-animation: sk-cubemove 1.8s infinite ease-in-out;
  animation: sk-cubemove 1.8s infinite ease-in-out;
}

.cube2 {
  -webkit-animation-delay: -0.9s;
  animation-delay: -0.9s;
}

@-webkit-keyframes sk-cubemove {
  25% { -webkit-transform: translateX(42px) rotate(-90deg) scale(0.5) }
  50% { -webkit-transform: translateX(42px) translateY(42px) rotate(-180deg) }
  75% { -webkit-transform: translateX(0px) translateY(42px) rotate(-270deg) scale(0.5) }
  100% { -webkit-transform: rotate(-360deg) }
}

@keyframes sk-cubemove {
  25% { 
    transform: translateX(42px) rotate(-90deg) scale(0.5);
    -webkit-transform: translateX(42px) rotate(-90deg) scale(0.5);
  } 50% { 
    transform: translateX(42px) translateY(42px) rotate(-179deg);
    -webkit-transform: translateX(42px) translateY(42px) rotate(-179deg);
  } 50.1% { 
    transform: translateX(42px) translateY(42px) rotate(-180deg);
    -webkit-transform: translateX(42px) translateY(42px) rotate(-180deg);
  } 75% { 
    transform: translateX(0px) translateY(42px) rotate(-270deg) scale(0.5);
    -webkit-transform: translateX(0px) translateY(42px) rotate(-270deg) scale(0.5);
  } 100% { 
    transform: rotate(-360deg);
    -webkit-transform: rotate(-360deg);
  }
} */


/* TURNING SQUARE SPINER */
.yams .loader {
  border: 2px solid #f3f3f3;
  border-radius: 0%;
  border-top: 4px solid 	rgb(160,160,160);
  border-right: 4px solid 	rgb(128,128,128);
  border-bottom: 4px solid 	rgb(190,190,190);
  border-left: 4px solid 	rgb(96,96,96);
  margin: auto;
  width: 15px;
  height: 15px;
  display: -webkit-inline-box;
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

.yams .image-checkbox > img {
	cursor: pointer;
  height: 100px;
  width: 100px;
}

.yams input[type="checkbox"] {
	display: none;
}

.yams input[type=checkbox]:checked + label > img{
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


  