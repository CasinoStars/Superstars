<template>
    <div id="wallet">
        <!-- errors of transactions !-->
        <div class="alert alert-danger" style="text-align: center;" v-if="errors.length > 0">
            <li v-for="e of errors" :key="e">{{e}}</li>
        </div>

        <!-- Upgrade -->

        <div class="form" style="letter-spacing: 2px; font-family: 'Courier New', sans-serif;">    
            <ul class="tab-group">
                <li style="color: white" class="tab active" v-if="this.wallet == 'real'"><a v-on:click="ChangeWallet('real')">Réel</a></li>
                <li style="color: white" class="tab" v-else><a v-on:click="ChangeWallet('real')">Réel</a></li>
                <li style="color: white" class="tab active" v-if="this.wallet == 'fake'"><a v-on:click="ChangeWallet('real')">Virtuel</a></li>
                <li style="color: white" class="tab" v-else><a v-on:click="ChangeWallet('fake')">Virtuel</a></li>
            </ul>
      
            <div class="tab-content">
                <h5 style="color: white" v-if="this.wallet == 'real'"><span style="font-weight: bold; font-style: italic;">Solde du compte:</span><br>{{trueCoins}} BTC</h5>
                <h5 style="color: white" v-if="this.wallet == 'real'"><span style="font-weight: bold; font-style: italic;">Adresse de dépots BTC:</span><br>{{BTCAddress}}<br><br></h5>
                <h5 style="color: white" v-else><span style="font-weight: bold; font-style: italic;">Solde du compte:</span><br>{{fakeCoins.balance}} all'in</h5>
                
                <form  @submit="Withdraw($event)" v-if="this.wallet == 'real'">
                        <div class="field-wrap">
                            <label>
                                Montant<span class="req">*</span>
                            </label><br><br>
                            <input  type="decimal" min="0" max="1000000" placeholder="Montant" v-model="item.AmountToSend" required autocomplete="off"/>
                        </div>
                    <div class="field-wrap">
                        <label>
                            Address<span class="req">*</span>
                        </label><br><br>
                        <input type="text" placeholder="Address" v-model="item.DestinationAddress" required autocomplete="off"/>
                    </div><br>      
                    <button type="submit" class="button button-block">Envoyer</button>
                </form>

                <form @submit="onSubmit($event)" :v-model="item.moneyType = 2" v-else>
                    <div class="field-wrap">
                        <label>
                            Montant<span class="req">*</span>
                        </label><br><br>
                        <input type="number" min="0" max="1000000" placeholder="..." v-model="item.fakeCoins" required autocomplete="off"/>
                    </div><br>
                    <button type="submit" class="button button-block">Créditer</button>

                </form>


        </div><!-- tab-content -->
        </div> <!-- /form -->
    </div>
</template>

<style lang="css">
  
</style>

<script>
import { mapActions } from 'vuex';
import WalletApiService from '../services/WalletApiService';

export default {
    data(){
        return {
            item: {},
            wallet: '',
            fakeCoins: 0,
            trueCoins: 'waiting...',
            BTCAddress: '',
            errors: [],
            Responses : [],
        };
    },

    mounted(){
        this.wallet = 'real';
        this.refreshTrueCoins();
        this.refreshFakeCoins();
        this.GetWalletAddress();
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),

        async GetWalletAddress(){
          this.BTCAddress = await this.executeAsyncRequest(() => WalletApiService.GetWalletAddress());
        },

        async refreshFakeCoins(){    
            this.fakeCoins = await this.executeAsyncRequest(() => WalletApiService.GetFakeBalance());
        },

        async refreshTrueCoins(){
            this.trueCoins = await this.executeAsyncRequest(() => WalletApiService.GetTrueBalance());
        },


        async onSubmit(e) {
            e.preventDefault();

                var errors = [];
                if(this.fakeCoins.balance >= 1000000)
                    errors.push("Le crédit est bloquer lorsque votre solde atteind 1,000,000 de bits");           
                this.errors = errors;

                if(errors.length == 0) {
                    try {
                        await this.executeAsyncRequest(() => WalletApiService.AddCoins(this.item));
                        await this.refreshFakeCoins();
                    }
                    catch(error) {
                    }
                }
        },

        async Withdraw(e) {
            e.preventDefault();
            console.log("Test");
            var errors = [];
                try {
                    this.Responses = await this.executeAsyncRequest(() => WalletApiService.Withdraw(this.item));
                    this.refreshTrueCoins();
                }
                catch(error){

                }
        },

        ChangeWallet(wallet){
            this.wallet = wallet;
            this.refreshFakeCoins();
            this.errors = 0;           
        },
    }
}
</script>

<style lang="scss">
// @import "compass/css3";

$body-bg: #c1bdba;
$form-bg: #13232f;
$white: #ffffff;

$main: #777c7b;
$main-light: lighten($main,5%);
$main-dark: darken($main,5%);

$gray-light: #a0b3b0;
$gray: #ddd;

$thin: 300;
$normal: 400;
$bold: 600;
$br: 4px;

// *, *:before, *:after {
//   box-sizing: border-box;
// }

html {
	overflow-y: scroll; 
}

body {
  background:$body-bg;
  font-family: 'Titillium Web', sans-serif;
}

a {
  text-decoration:none;
  color:$main;
  transition:.5s ease;
  &:hover {
    color:$main-dark;
  }
}

.form {
  background:rgba($form-bg,.9);
  padding: 40px;
  max-width:600px;
  margin:40px auto;
  border-radius:$br;
  box-shadow:0 4px 10px 4px rgba($form-bg,.3);
}

.tab-group {
  list-style:none;
  padding:0;
  margin:0 0 40px 0;
  &:after {
    content: "";
    display: table;
    clear: both;
  }
  li a {
    display:block;
    text-decoration:none;
    padding:15px;
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

.tab-content > div:last-child {
  display:none;
}


h5 {
  text-align:center;
  color:$white;
  font-weight:$thin;
}

label {
  position:absolute;
  transform:translateY(6px);
  left:13px;
  color:rgba($white,.5);
  transition:all 0.25s ease;
  -webkit-backface-visibility: hidden;
  pointer-events: none;
  font-size:22px;
  .req {
  	margin:2px;
  	color:#1ab188;;
  }
}

input, textarea {
  font-size:22px;
  display:block;
  width:100%;
  height:100%;
  padding:5px 10px;
  background:none;
  background-image:none;
  border:1px solid $gray-light;
  color:$white;
  border-radius:0;
  transition:border-color .25s ease, box-shadow .25s ease;
  &:focus {
		outline:0;
		border-color:$main;
  }
}

textarea {
  border:2px solid $gray-light;
  resize: vertical;
}

.field-wrap {
  position:relative;
  margin-bottom:10px;
}


.button {
  border:0;
  outline:none;
  border-radius:0;
  padding:15px 0;
  font-size:2rem;
  font-weight:$bold;
  text-transform:uppercase;
  letter-spacing:.1em;
  background:$main;
  color:$white;
  transition:all.5s ease;
  -webkit-appearance: none;
  &:hover, &:focus {
    background:$main-dark;
  }
}

.button-block {
  display:block;
  width:100%;
}

.forgot {
  margin-top:-20px;
  text-align:right;
}
</style>
