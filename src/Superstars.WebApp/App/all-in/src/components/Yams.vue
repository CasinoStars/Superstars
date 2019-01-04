<template>
<div class="yams">
  <!-- The Modal -->
  <div id="myModal" class="modal">
    <!-- Modal content -->
    <div class="modal-content">
      
      <div class="modal-header">
        <div style="margin-left: 20%; padding-top: 2px; font-family: 'Courier New', sans-serif;">
          <h2 v-if="realOrFake == 'real'">SOLDE DE VOTRE COMPTE BTC: {{BTCMoney.toLocaleString('en')}} <i class="fa fa-btc" style="font-size: 1.5rem;"></i></h2>
          <h2 v-else>SOLDE DE VOTRE COMPTE ALL'IN: {{fakeMoney.toLocaleString('en')}} <i class="fa fa-money" style="font-size: 1.5rem;"></i></h2>
        </div>
        <router-link class="close" v-on:click.native="RedirectandDelete()" to="">&times;</router-link>
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
            <router-link class="btn btn-secondary" v-on:click.native="RedirectandDelete()" to="">Annuler</router-link>
            <button type="submit" class="btn btn-light">Confirmer</button>
          </div>
        </div>
      </form>
    </div>
  </div>
  
  <h3 style="letter-spacing: 2px; font-family: 'Courier New', sans-serif; margin-top:2%; margin-left:2%;">POT: {{pot.toLocaleString('en')}}<i v-if="this.realOrFake == 'real'" class="fa fa-btc" style="font-size: 1.5rem;"/><i v-else class="fa fa-money" style="font-size: 1.5rem;"/></h3>
  <h3 style="letter-spacing: 2px; font-family: 'Courier New', sans-serif; margin-top: -3%; margin-left:89%;">TOUR: {{nbTurn}}</h3>
  <h3 style="letter-spacing: 2px; font-family: 'Courier New', sans-serif; margin-top:2%; margin-left:2%;">Score du Joueur: {{playerScore}}</h3>
  <h3 style="letter-spacing: 2px; font-family: 'Courier New', sans-serif; margin-top: -3%; margin-left:75%;">Score de l'IA: {{IaScore}}</h3>
  <h3 style="letter-spacing: 2px; font-family: 'Courier New', sans-serif; margin-top:2%; margin-left:2%;">Figure du Joueur: {{playerFigure}}</h3>
  <h3 style="letter-spacing: 2px; font-family: 'Courier New', sans-serif; margin-top: -3%; margin-left:75%;">Figure de l'IA: {{IaFigure}}</h3>




  
  <form @submit="onSubmitAI($event)" id="PlayAI">
    <div v-for="(i, index) of iadices" :key="index" class="iadices">
      <img :src="getDiceIaImage(i, index)">
    </div>
  </form>
  
  <br><br>
  <div id="tutorialRectangle" class="bg-dark" v-if="playerBet == true && nbTurn == 0 && wins == 0">
    <p id="tutorialText"> {{tutorialp}}</p>
    <button class="btn btn-secondary active" id="tutorialButton" v-on:click="OkTutorial()"> Ok ! </button>
  </div>
  <br><br>
  
  <form @submit="onSubmit($event)" id="PlayPlayer">
    <div v-for="(i, index) of dices" :key="index" class="playerdices">
      <input type="checkbox" :id="index+1" :value="index+1" v-model="selected" v-if="nbTurn != 0 && nbTurn < 3">
      <label class="image-checkbox" :for="index+1">
        <img :src="getDiceImage(i, index)" :id="index">
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
    <div id="snackbar">{{success}} <i style="color:green" class="fa fa-check"></i></div>
</div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex';
import YamsApiService from '../services/YamsApiService';
import GameApiService from '../services/GameApiService';
import WalletApiService from '../services/WalletApiService';
import UserApiService from '../services/UserApiService';
import Vue from 'vue';

export default {
  data(){
    return {
      selected: [],
      dices: [],
      iadices: [],
      indexRerollDicesIa: [],
      nbTurn: 0,
      nbTurnIa: 0,
      winOrLose: '',
      playerFigure: '',
      IaFigure: '',
      playerScore: '',
      IaScore: '',
      wait: '',
      playerwin: '',
      playerBet: false,
      realOrFake: 'real',
      profit: 0,
      fakeBet: 0,
      trueBet: 0,
      errors: [],
      success: '',
      iaRollDices: false,
      rollDices: false,
      pot: 0,
      nbSlidesTutorial: 0,
      tutorialp: '',
      queryPseudo: '',
      wins: 0
    }
  },

  async mounted() {

    if(this.$route.query.pseudo)
      this.queryPseudo = this.$route.query.pseudo;
    else
      this.queryPseudo = UserApiService.pseudo;

    this.wins = await this.executeAsyncRequest(() => GameApiService.getWinsYamsPlayer(this.queryPseudo));

    await this.refreshDices();
    await this.refreshIaDices();
    await this.changeTurn();
    this.pot = await this.executeAsyncRequest(() => GameApiService.getYamsPot());
    if(this.pot == 0) {
      this.showModal();
    } else {
      this.playerBet = true;
    }  
      this.tutorialp = "Bienvenue sur le Yams !";
  },

  computed: {
    ...mapGetters(['BTCMoney']),
    ...mapGetters(['fakeMoney'])
  },

  methods: {
    ...mapActions(['executeAsyncRequest']),
    ...mapActions(['RefreshFakeCoins']),
    ...mapActions(['RefreshBTC']),
    
    async RedirectandDelete() {
      //await this.executeAsyncRequest(() => GameApiService.deleteYamsGame());
      await this.executeAsyncRequest(() => GameApiService.deleteGame(0));
      this.$router.push({ path: 'play' });
    },

    OkTutorial() {
      let rectangle = document.getElementById("tutorialRectangle");
      
      this.nbSlidesTutorial = this.nbSlidesTutorial + 1;
      if(this.nbSlidesTutorial === 1 ) {
          this.tutorialp = "  Vous allez devoir réaliser la meilleure figure possible avec vos 5 dés ";
      } else if(this.nbSlidesTutorial === 2) {
        this.tutorialp = "  Vous disposez de 3 essais pour relancer n'importe lesquels de vos dés ";
      } else if(this.nbSlidesTutorial === 3) {
        this.tutorialp = "  L'ordinateur jouera après vous en suivant ces mêmes règles" ;
      } else if(this.nbSlidesTutorial === 4) {
        this.tutorialp = "  Celui ayant la meilleure figure remporte la partie ! Bonne chance ! ";
      } else if(this.nbSlidesTutorial === 5) {
          rectangle.classList.toggle('fade');
      }
    },

    changeBet(choice) {
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
        else if(this.fakeBet > this.fakeMoney)
          errors.push("Vous n'avez pas cette somme sur votre compte");
      }
      else {
        if(this.trueBet > 10000000)
          errors.push("La mise maximum est de 10,000,000 bits");
        else if(this.trueBet <= 0)  
          errors.push("La mise doit être supérieur à 0 bits");
        else if(this.trueBet > this.BTCMoney){
          errors.push("Vous n'avez pas cette somme sur votre compte");}
      }
      this.errors = errors;
      if(errors.length == 0) {
        try {
          if(this.realOrFake === 'fake') {
            await this.executeAsyncRequest(() => GameApiService.BetFake(this.fakeBet, 0));
            await this.RefreshFakeCoins();
            this.success = 'Vous venez de parier: '+this.fakeBet+' All`In Coins';
          }
          else {
            await this.executeAsyncRequest(() => GameApiService.BetBTC(this.trueBet, 0));
            await this.RefreshBTC();
            this.success = 'Vous venez de parier: '+this.trueBet+' Bits';
          }
          var x = document.getElementById("snackbar");
          x.className = "show";
          setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);
          var modal = document.getElementById('myModal');
          modal.style.display = "none";
          this.pot = await this.executeAsyncRequest(() => GameApiService.getYamsPot());
          this.playerBet = true;
        }
        catch(error) {
        }
      }
    },

    async refreshDices() {
      this.dices = await this.executeAsyncRequest(() => YamsApiService.GetPlayerDices());
      this.rollDices = false;
      this.selected = [];
      if(this.nbTurn < 3)
        await this.changeTurn();
      var result = await  this.executeAsyncRequest(() => YamsApiService.GetScore());
      this.playerScore = result[0];
      this.playerFigure = result[1];
      this.IaScore = result[2];
      this.IaFigure = result[3];
    },
    
    async refreshIaDices() {
      this.iadices = await this.executeAsyncRequest(() => YamsApiService.GetIaDices());
      
    },

    async changeTurn() {
      this.nbTurn = await this.executeAsyncRequest(() => YamsApiService.GetTurn());
    },

    async updateStats(moneyType, bet) {
      await this.executeAsyncRequest(() => GameApiService.UpdateStats(0, moneyType, bet, this.playerwin));
      await this.executeAsyncRequest(() => GameApiService.gameEndUpdate(0, this.playerwin, this.realOrFake));
      await this.executeAsyncRequest(() => YamsApiService.DeleteYamsAiPlayer());
      await this.executeAsyncRequest(() => GameApiService.DeleteAis(0));
    },

    async getFinalResult() {
      let tableResult = await this.executeAsyncRequest(() => YamsApiService.GetFinalResult());
      this.IaFigure = tableResult[0];
      this.playerFigure = tableResult[1];
      this.winOrLose = tableResult[2];
      var pot = await this.executeAsyncRequest(() => GameApiService.getYamsPot());
      if(this.winOrLose == "You Lose") {
          this.playerwin = 'AI';
          if(this.trueBet === 0)
            await this.updateStats(0, this.fakeBet);
          else
            await this.updateStats(1, this.trueBet);
      }
      else if(this.winOrLose == "You Win"){
        this.playerwin = 'Player';
          if(this.trueBet === 0) {
            await this.executeAsyncRequest(() => WalletApiService.WithdrawFakeBankRoll(pot));
            await this.executeAsyncRequest(() => WalletApiService.CreditPlayerInFake(pot));
            await this.RefreshFakeCoins();
            await this.updateStats(0, this.fakeBet);
          }
          else {
            await this.executeAsyncRequest(() => WalletApiService.WithdrawBTCBankRoll(pot));
            await this.executeAsyncRequest(() => WalletApiService.CreditPlayerInBTC(pot));
            await this.RefreshBTC();
            await this.updateStats(1, this.trueBet);
          }
      } else {
        this.playerwin = 'Equality';
        await this.executeAsyncRequest(() => YamsApiService.DeleteYamsAiPlayer());
        await this.executeAsyncRequest(() => GameApiService.DeleteAis(0));
         if(this.trueBet === 0) {
            await this.executeAsyncRequest(() => WalletApiService.WithdrawFakeBankRoll(pot/2));
            await this.executeAsyncRequest(() => WalletApiService.CreditPlayerInFake(pot/2));
            await this.RefreshFakeCoins();
            await this.updateStats(0,0);
          }
          else {
            await this.executeAsyncRequest(() => WalletApiService.WithdrawBTCBankRoll(pot/2));
            await this.executeAsyncRequest(() => WalletApiService.CreditPlayerInBTC(pot/2));
            await this.RefreshBTC();
            await this.updateStats(1,0);
          }
      }
    },

    async RePlay() {
      await this.executeAsyncRequest(() => YamsApiService.DeleteYamsAiPlayer());
      await this.executeAsyncRequest(() => GameApiService.createGame(0));
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
        this.iaRollDices = true;
        let arraydice = [this.iadices, this.dices];
        this.nbTurnIa = this.nbTurnIa + 1;
        var dicesAndIndex = await this.executeAsyncRequest(() => YamsApiService.RollIaDices(arraydice)).then(r => r.json());
        this.iadices = dicesAndIndex[0];
        this.indexRerollDicesIa = dicesAndIndex[1];
        console.log(this.indexRerollDicesIa);
        await new Promise(f => setTimeout(f, 1500));
        //await this.refreshIaDices();
        this.iaRollDices = false;
        this.indexRerollDicesIa = []
        await new Promise(f => setTimeout(f, 1500));
        var result = await  this.executeAsyncRequest(() => YamsApiService.GetScore());
      
      this.IaScore = result[2];
      this.IaFigure = result[3];
      console.log(result);
        
      }
      if(this.nbTurnIa === 3)
        await this.getFinalResult();
      
    },

    async onSubmit(e) {
      e.preventDefault();
      this.rollDices = true;
      if(this.nbTurn === 0) {
        this.selected = [1,2,3,4,5];
        await this.executeAsyncRequest(() => YamsApiService.RollDices(this.selected));
      }
      else
        await this.executeAsyncRequest(() => YamsApiService.RollDices(this.selected));
      await new Promise(f => setTimeout(f, 1500));
      await this.refreshDices();
    },

    getDiceIaImage(value, index) {
      let image = this.indexRerollDicesIa.findIndex(x => x === index) !== -1 && this.iaRollDices ? `diceIaRoll.gif` : `diceia${value}.png`;
      return require(`../img/${image}`);
    },

    getDiceImage(value, index) {
      let image = this.selected.findIndex(x => x === index + 1) !== -1 && this.rollDices ? `dicePlayerRoll.gif` : `dice${value}.png`;
      return require(`../img/${image}`);
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

#tutorialRectangle {
   width: 60%; 
   height: 50%;
  //  background: lightgrey;
   margin-left: 18.8%;
   margin-top: -11.5%;
   border-radius: 20px;
   text-align: center;
   opacity: 0.99;
   position: absolute; 
   transition: opacity 1s; 
   z-index: 15;
}

#tutorialRectangle.fade {
  visibility: hidden;
  opacity: 0;
  transition: visibility 0s 2s, opacity 2s linear;
}

#tutorialText {
color:white;
text-transform: uppercase;
font-size:24px;
font-family: 'Courier New', sans-serif;
text-align: center;
position: relative;
margin-top: 5%;
}

#tutorialButton {
    text-align: center;
    text-transform: uppercase;
    font-family: 'Courier New', sans-serif;
    display: inline-block;
    font-size: 22px;
    border-radius: 3px;
    position: relative;
    margin-top: 5%;
}

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