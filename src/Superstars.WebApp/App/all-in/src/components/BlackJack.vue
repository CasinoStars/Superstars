<template>
<div class="blackJack">

<div id="myModal" class="modal">
    <!-- Modal content -->
    <div class="modal-content">

      <div class="modal-header">
        <div style="margin-left: 20%; padding-top: 2px; font-family: 'Courier New', sans-serif;">
          <h2 v-if="realOrFake == 'real'">SOLDE DE VOTRE COMPTE BTC: {{BTCMoney}} <i class="fa fa-btc" style="font-size: 1.5rem;"></i></h2>
          <h2 v-else>SOLDE DE VOTRE COMPTE ALL'IN: {{fakeMoney}} <i class="fa fa-money" style="font-size: 1.5rem;"></i></h2>
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
          <input style="margin-top: 10px; margin-bottom: 1%;" v-if="realOrFake == 'real'" type="decimal" v-model="trueBet" required/>
          <input style="margin-top: 10px; margin-bottom: 1%;" v-else type="number" v-model="fakeBet" required/>
        
          <div class="alert alert-danger" style="opacity: 0.8; font-weight: bold; font-family: 'Courier New', sans-serif; text-transform: uppercase; margin-top: 1%; margin-bottom: 0.5%; text-align: center; font-family: 'Courier New', sans-serif;" v-if="errors.length > 0">
            <div style="opacity: 0.7;" v-for="e of errors" :key="e">{{e}}</div>
          </div>
        </div>

        <div class="modal-footer">
          <div style="margin-right: 42%;">
            <router-link class="btn btn-secondary"  v-on:click.native="setisingamefalseandredirect()" to="">Annuler</router-link>
            <button type="submit" class="btn btn-light">Confirmer</button>
          </div>
        </div>
      </form>
    </div>
  </div>

<center>
    <a class="txt"> Cartes du Dealer </a>
<div id="leespace3"></div>
    <div v-for="(i, index) of dealercards" :key="index" class="dealercards" v-if="playerBet">
      <img v-if="i == '2p'" src="../img/2p.png">
      <img v-if="i == '3p'" src="../img/3p.png">
      <img v-if="i == '4p'" src="../img/4p.png">
      <img v-if="i == '5p'" src="../img/5p.png">      
      <img v-if="i == '6p'" src="../img/6p.png">
      <img v-if="i == '7p'" src="../img/7p.png">      
      <img v-if="i == '8p'" src="../img/8p.png">
      <img v-if="i == '9p'" src="../img/9p.png">      
      <img v-if="i == '10p'" src="../img/10p.png">
      <img v-if="i == '11p'" src="../img/11p.png">      
      <img v-if="i == '12p'" src="../img/12p.png">
      <img v-if="i == '13p'" src="../img/13p.png">
      <img v-if="i == '14p'" src="../img/1p.png">

      <img v-if="i == '2h'" src="../img/2h.png">
      <img v-if="i == '3h'" src="../img/3h.png">
      <img v-if="i == '4h'" src="../img/4h.png">
      <img v-if="i == '5h'" src="../img/5h.png">      
      <img v-if="i == '6h'" src="../img/6h.png">
      <img v-if="i == '7h'" src="../img/7h.png">      
      <img v-if="i == '8h'" src="../img/8h.png">
      <img v-if="i == '9h'" src="../img/9h.png">      
      <img v-if="i == '10h'" src="../img/10h.png">
      <img v-if="i == '11h'" src="../img/11h.png">      
      <img v-if="i == '12h'" src="../img/12h.png">
      <img v-if="i == '13h'" src="../img/13h.png">
      <img v-if="i == '14h'" src="../img/1h.png">

      <img v-if="i == '2t'" src="../img/2t.png">
      <img v-if="i == '3t'" src="../img/3t.png">
      <img v-if="i == '4t'" src="../img/4t.png">
      <img v-if="i == '5t'" src="../img/5t.png">      
      <img v-if="i == '6t'" src="../img/6t.png">
      <img v-if="i == '7t'" src="../img/7t.png">      
      <img v-if="i == '8t'" src="../img/8t.png">
      <img v-if="i == '9t'" src="../img/9t.png">      
      <img v-if="i == '10t'" src="../img/10t.png">
      <img v-if="i == '11t'" src="../img/11t.png">      
      <img v-if="i == '12t'" src="../img/12t.png">
      <img v-if="i == '13t'" src="../img/13t.png">
      <img v-if="i == '14t'" src="../img/1t.png">

      <img v-if="i == '2c'" src="../img/2c.png">
      <img v-if="i == '3c'" src="../img/3c.png">
      <img v-if="i == '4c'" src="../img/4c.png">
      <img v-if="i == '5c'" src="../img/5c.png">      
      <img v-if="i == '6c'" src="../img/6c.png">
      <img v-if="i == '7c'" src="../img/7c.png">      
      <img v-if="i == '8c'" src="../img/8c.png">
      <img v-if="i == '9c'" src="../img/9c.png">      
      <img v-if="i == '10c'" src="../img/10c.png">
      <img v-if="i == '11c'" src="../img/11c.png">      
      <img v-if="i == '12c'" src="../img/12c.png">
      <img v-if="i == '13c'" src="../img/13c.png">
      <img v-if="i == '14c'" src="../img/1c.png">
    </div>
    <div v-else class="dealercards">
        <img src="../img/back.png" id="deck"/>
    </div>        
</center>



<div id="tocenter">

<div id="infos">
    <a class="txt"> Valeur de votre main : {{handvalue}} </a> <br>
    <a class="txt"> Valeur de la main du dealer : {{dealerhandvalue}} </a> <br>
    <a v-if="!iaturn && !gameend" class="txt"> C'est à votre tour de jouer </a> <br>
    <a v-if="dealerplaying && !gameend" class="txt"> Le dealer est entrain de jouer </a> <br>
    <a v-if="gameend" class="txt"> <strong> {{winnerlooser}} </strong> </a> <br>
</div>

<div id="wait" >
<div v-if="!gameend && dealerplaying" class="lds-css ng-scope">
  <div style="width:100%;height:100%" class="lds-eclipse">
    <div>
      </div>
      </div>
      </div>
</div>

<div id="deck1">
    <img src="../img/back.png" id="deck"/>
</div>

</div>

    <center>
    <a class="txt"> Votre main </a>
<div id="leespace3"></div>

    <div v-for="(i, index) of playercards" :key="index" class="playercards" v-if="playerBet">
      <img v-if="i == '2p'" src="../img/2p.png">
      <img v-if="i == '3p'" src="../img/3p.png">
      <img v-if="i == '4p'" src="../img/4p.png">
      <img v-if="i == '5p'" src="../img/5p.png">      
      <img v-if="i == '6p'" src="../img/6p.png">
      <img v-if="i == '7p'" src="../img/7p.png">      
      <img v-if="i == '8p'" src="../img/8p.png">
      <img v-if="i == '9p'" src="../img/9p.png">      
      <img v-if="i == '10p'" src="../img/10p.png">
      <img v-if="i == '11p'" src="../img/11p.png">      
      <img v-if="i == '12p'" src="../img/12p.png">
      <img v-if="i == '13p'" src="../img/13p.png">
      <img v-if="i == '14p'" src="../img/1p.png">

      <img v-if="i == '2h'" src="../img/2h.png">
      <img v-if="i == '3h'" src="../img/3h.png">
      <img v-if="i == '4h'" src="../img/4h.png">
      <img v-if="i == '5h'" src="../img/5h.png">      
      <img v-if="i == '6h'" src="../img/6h.png">
      <img v-if="i == '7h'" src="../img/7h.png">      
      <img v-if="i == '8h'" src="../img/8h.png">
      <img v-if="i == '9h'" src="../img/9h.png">      
      <img v-if="i == '10h'" src="../img/10h.png">
      <img v-if="i == '11h'" src="../img/11h.png">      
      <img v-if="i == '12h'" src="../img/12h.png">
      <img v-if="i == '13h'" src="../img/13h.png">
      <img v-if="i == '14h'" src="../img/1h.png">

      <img v-if="i == '2t'" src="../img/2t.png">
      <img v-if="i == '3t'" src="../img/3t.png">
      <img v-if="i == '4t'" src="../img/4t.png">
      <img v-if="i == '5t'" src="../img/5t.png">      
      <img v-if="i == '6t'" src="../img/6t.png">
      <img v-if="i == '7t'" src="../img/7t.png">      
      <img v-if="i == '8t'" src="../img/8t.png">
      <img v-if="i == '9t'" src="../img/9t.png">      
      <img v-if="i == '10t'" src="../img/10t.png">
      <img v-if="i == '11t'" src="../img/11t.png">      
      <img v-if="i == '12t'" src="../img/12t.png">
      <img v-if="i == '13t'" src="../img/13t.png">
      <img v-if="i == '14t'" src="../img/1t.png">

      <img v-if="i == '2c'" src="../img/2c.png">
      <img v-if="i == '3c'" src="../img/3c.png">
      <img v-if="i == '4c'" src="../img/4c.png">
      <img v-if="i == '5c'" src="../img/5c.png">      
      <img v-if="i == '6c'" src="../img/6c.png">
      <img v-if="i == '7c'" src="../img/7c.png">      
      <img v-if="i == '8c'" src="../img/8c.png">
      <img v-if="i == '9c'" src="../img/9c.png">      
      <img v-if="i == '10c'" src="../img/10c.png">
      <img v-if="i == '11c'" src="../img/11c.png">      
      <img v-if="i == '12c'" src="../img/12c.png">
      <img v-if="i == '13c'" src="../img/13c.png">
      <img v-if="i == '14c'" src="../img/1c.png">
    </div>
        <div v-else class="playercards">
        <img src="../img/back.png" id="deck"/>
    </div>  
    </center>
<div id="leespace2"></div>
   <form @submit="hit($event)">
   <div style="text-align:center;"><button type="submit" value="hit" class="btn btn-outline-secondary btn-lg" v-if="handvalue < 21 && iaturn == false && gameend == false ">HIT</button></div>
   </form>
<div id="leespace"></div>
   <form @submit="stand($event)">
   <div style="text-align:center;"><button type="submit" value="stand" class="btn btn-outline-secondary btn-lg" v-if="handvalue < 21 && iaturn == false && gameend == false">STAND</button></div>
   </form>

   <!-- <form @submit="split($event)">
   <div style="text-align:center;"><button type="submit" value="split" class="btn btn-outline-secondary btn-lg" v-if="hasplitplayer == false && cansplitplayer && iaturn == false && gameend == false && handvalue < 21">SPLIT</button></div>
   </form> -->

   <!-- <form>
   <div><button type="button" class="btn btn-lg btn-primary" disabled>Primary button</button></div>
   <div><button type="button" class="btn btn-secondary btn-lg" disabled>Button</button></div>
   </form> -->

   <form @submit="playdealer($event)">
   <div style="text-align:center;"><button  type="submit" value="playdealer" class="btn btn-outline-secondary btn-lg" v-if="dealerhandvalue < 21 && iaturn == true && gameend == false">PLAY AI</button></div>
   </form>
<!-- v-on:click="showait()" -->

    <router-link to="/play">
      <br><div style="text-align:center;"><button  style="text-align:center;" class="btn btn-dark" v-if="gameend == true">QUITTER</button></div>
    </router-link>
   </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex';
import BlackJackApiService from '../services/BlackJackApiService';
import Vue from 'vue';
import GameApiService from '../services/GameApiService';
import WalletApiService from '../services/WalletApiService';


export default {
    data() {
        return {
            isingame: 0,
            playercards: [],
            dealercards: [],
            handvalue: 0,
            dealerhandvalue: 0,
            iaturn : false,
            dealerplaying: false,
            gameend: false,
            winnerlooser: '',
            playerwin: false,
            nbturn: 0,
            nbhit: 0,
            playerBet: false,
            realOrFake: 'real',
            fakeBet: 0,
            trueBet: 0,
            errors: [],
        }
    },
  computed: {
    ...mapGetters(['BTCMoney']),
    ...mapGetters(['fakeMoney'])
  },

  async mounted() {
    await this.getFakeCoins();
    await this.getTrueCoins();
    await this.getIsingame();
    if(this.isingame == 0) {
      this.showModal();
      await this.setisingametrue();
    } else {
    this.refreshiaturn();
    }
    this.nbturn = await this.executeAsyncRequest(() => BlackJackApiService.GetTurn());
    await this.refreshCards();
    await this.refreshHandValue();
    await this.CheckWinner();
    console.log(this.handvalue + "   HANDVALUE");
    console.log(this.dealerhandvalue + " DEALERHANDVALUE");
    console.log(this.iaturn + "  IATURN");
    console.log(this.gameend + " GAME END");
  },

    methods: {
      ...mapActions(['executeAsyncRequest']),
      ...mapActions(['RefreshFakeCoins']),
      ...mapActions(['RefreshBTC']),

    async setisingametrue() {
        await this.executeAsyncRequest(() => BlackJackApiService.SetIsingameBJ(1));
    },

    async setisingamefalse() {
        await this.executeAsyncRequest(() =>  BlackJackApiService.SetIsingameBJ(0));
    },

    async setisingamefalseandredirect() {
        await this.executeAsyncRequest(() => BlackJackApiService.SetIsingameBJ(0));
        this.$router.push({ path: 'play' });
    },

    async getIsingame() {
      this.isingame = await this.executeAsyncRequest(() => BlackJackApiService.Getisingame());
      if(this.isingame == 1) {
        this.playerBet = true;
      }
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
        if(this.fakeBet > 10000000)
          errors.push("La mise maximum est de 10,000,000");
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
          if(this.realOrFake === 'fake') {
            await this.executeAsyncRequest(() => GameApiService.BetFake(this.fakeBet, 'BlackJack'));
            await this.RefreshFakeCoins();
          }
          else {
            await this.executeAsyncRequest(() => GameApiService.BetBTC(this.trueBet, 'BlackJack'));
            await this.RefreshBTC();
          }
          var modal = document.getElementById('myModal');
          modal.style.display = "none";
          this.playerBet = true;
        }
        catch(error) {
        }
      }
    },

    async hit(e) {
      e.preventDefault();
      await this.executeAsyncRequest(() => BlackJackApiService.HitPlayer());

      if(this.handvalue > 21) {
        this.gameend = true;
      }
      this.nbturn = await this.executeAsyncRequest(() => BlackJackApiService.GetTurn());
      await this.refreshCards();
      await this.refreshHandValue();
      await this.CheckWinner();
    },

    async stand(e) {
      e.preventDefault();
      this.iaturn =  true;
      await this.StandandFinish();
      await this.refreshCards();
      await this.refreshHandValue();
    },


    async refreshiaturn() {
        this.iaturn = await this.executeAsyncRequest(() => BlackJackApiService.refreshAiturn());
    },

    async StandandFinish() {
      await this.executeAsyncRequest(() => BlackJackApiService.StandPlayer());
      await this.CheckWinner();
    },

    async playdealer(e) {
      e.preventDefault();
      document.getElementById('wait').style.visibility = "visible";
      setTimeout(this.playdealersecond,2000);
    },

    async playdealersecond() {
      this.dealerplaying = true;
      await this.executeAsyncRequest(() => BlackJackApiService.PlayAI());
      await this.refreshCards();
      await this.refreshHandValue();
      this.gameend = true;
      await this.CheckWinner();
    },

    async CheckWinner() {
      if(this.gameend == true || this.handvalue == this.dealerhandvalue && this.iaturn == true || this.handvalue == 21 || this.handvalue > 21 || this.dealerhandvalue == 21 || this.dealerhandvalue > 21) {
        if(this.handvalue > 21 ) {
          this.winnerlooser = 'Vous avez perdu !';
        } else if(this.dealerhandvalue > 21) {
          this.winnerlooser = 'Vous avez gagné !'
          this.playerwin = true;
        } else if(this.dealerhandvalue == 21) {
          this.winnerlooser = 'BLACKJACK! Vous avez perdu !'
        } else if(this.handvalue == 21) {
          this.winnerlooser = 'BLACKJACK ! Vous avez gagné !';
          this.playerwin = true;
        } else if(this.handvalue < this.dealerhandvalue ) {
          this.winnerlooser = 'Vous avez perdu !';
        }  else if(this.handvalue > this.dealerhandvalue) {
          this.winnerlooser = 'Vous avez gagné !';
          this.playerwin = true;
        } else if(this.handvalue == this.dealerhandvalue) {
          this.winnerlooser = "Egalité";
        }
        this.gameend = true;
        if(this.playerwin == true) {
          var pot = await this.executeAsyncRequest(() => GameApiService.getBlackJackPot());
          if(this.trueBet === 0) {
            await this.executeAsyncRequest(() => WalletApiService.WithdrawFakeBankRoll(pot));
            await this.executeAsyncRequest(() => WalletApiService.CreditPlayerInFake(pot));
            await this.RefreshFakeCoins();
          }
          else {
            await this.executeAsyncRequest(() => WalletApiService.WithdrawBTCBankRoll(pot));
            await this.executeAsyncRequest(() => WalletApiService.CreditPlayerInBTC(pot));
            await this.RefreshBTC();
          }
        }
        this.updateStats();
      }
    },

    async updateStats() {
        await this.setisingamefalse();
        await this.executeAsyncRequest(() => GameApiService.UpdateStats('BlackJack',this.playerwin));
    },

    async refreshHandValue() {
      this.handvalue = await this.executeAsyncRequest(() => BlackJackApiService.getplayerHandValue());
      this.dealerhandvalue = await this.executeAsyncRequest(() => BlackJackApiService.getAiHandValue());
    },

    async refreshCards() {
      this.playercards = await this.executeAsyncRequest(() => BlackJackApiService.GetPlayerCards());
      this.dealercards = await this.executeAsyncRequest(() => BlackJackApiService.GetAiCards());
    },
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

.blackJack .tab-group {
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
.blackJack .modal {
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
.blackJack .modal-content {
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
.blackJack .close {
    color: white;
    float: right;
    font-size: 28px;
    font-weight: bold;
}

.blackJack .close:hover,
.blackJack .close:focus {
    color: white;
    text-decoration: none;
    cursor: pointer;
}

.blackJack.req {
	margin:2px;
	color:#1ab188;;
}

.blackJack .modal-header {
    padding: 10px 16px;
    text-align: center;
    background: #222222a8;
    color: white;
}

.blackJack .modal-body {
  padding: 20px 16px;
  text-align: center;
}

.blackJack .modal-footer {
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
.lds-eclipse {
  position: relative;
}

.lds-eclipse div {
  position: absolute;
  -webkit-animation: lds-eclipse 1s linear infinite;
  animation: lds-eclipse 1s linear infinite;
  width: 40px;
  height: 40px;
  top: 80px;
  left: 80px;
  border-radius: 50%;
  box-shadow: 0 2px 0 0 #7f8387;
  -webkit-transform-origin: 20px 21px;
  transform-origin: 20px 21px;
}
.lds-eclipse {
  width: 200px !important;
  height: 200px !important;
  -webkit-transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
  transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
  margin-left: 50%;
}

.txt {
  font-family: 'Courier New', sans-serif;
  font-variant: small-caps;
  color: rgb(0, 0, 0);
  font-size: 28px;
}

#tocenter {
    display: -webkit-inline-box;
}

#infos {
margin-top: 3%;
padding-left: 1%;
}

 #deck {
height: 215px;
width: 135px;
 }


#deck1 {
    margin-left: 680px;
}

#wait {
    margin-left: 300px;
    visibility: hidden;
}

 .playercards > img {
     height: 205px;
     width: 125px;
 }
 
 .playercards {
	display: inline-block;
	position: relative;
	
 }

 .dealercards {
	display: inline-block;
	position: relative;
 }

 .dealercards > img {
     height: 205px;
     width: 125px;

 }

#leespace {
    height: 5px;
    width: 5px;
}

#leespace2 {
    height: 20px;
    width: 20px;
}

#leespace3 {
    height: 10px;
    width: 10px;
}
</style>



