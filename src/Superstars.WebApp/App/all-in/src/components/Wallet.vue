<template>
    <div class="wallet">

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
                <h5 style="color: white" v-if="this.wallet == 'real'"><span style="font-weight: bold; font-style: italic;">Solde du compte:</span><br>{{BTCMoney}} BTC</h5>
                <h5 style="color: white" v-if="this.wallet == 'real'"><span style="font-weight: bold; font-style: italic;">Adresse de dépots BTC:</span><br>{{BTCAddress}} <a style="cursor: pointer;" @click="Copy()"><i class="fa fa-files-o"></i></a><br><br></h5>
                <h5 style="color: white" v-else><span style="font-weight: bold; font-style: italic;">Solde du compte:</span><br>{{fakeMoney}} all'in</h5>
  <div id="withdrawModal" class="modal">
    <!-- Modal content -->
    <div class="modal-content">

      <div class="modal-header">
        <div style="margin-left: 20%; padding-top: 2px; font-family: 'Courier New', sans-serif;">
          <p >CONFIRMATION DE RETRAIT:  <i class="fa fa-btc" style="font-size: 1.5rem;"></i></p>
        </div>
      </div>
        <div  class="modal-body">    
        <h4 style="color: white;">{{Responses}} </h4>  
            <div style="opacity: 0.7;" v-for="e of errors" :key="e">{{e}}</div>
        </div>
        <div class="modal-footer">
          <div style="margin-right: 42%;">
              
            <button v-on:click="closeModal()" type="close" class="btn btn-light">Confirmer</button>
          </div>
        </div>
      </div>
    </div>
  </div>
                <!-- RealWallet -->
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
                    <div> </div>
                </form>

                <!-- FakeWallet -->
                <form @submit="onSubmit($event)" :v-model="item.MoneyType = 0" v-else>
                    <div class="field-wrap">
                        <label>
                            Montant<span class="req">*</span>
                        </label><br><br>
                        <input type="number" min="0" max="1000000" placeholder="..." v-model="item.AmountToSend" required autocomplete="off"/>
                    </div><br>
                    <button type="submit" class="button button-block">Créditer</button>

                </form>               
            </div>
        </div>
         <!-- The Modal -->
</template>

<style lang="css">
</style>

<script>
import { mapActions, mapGetters } from "vuex";
import WalletApiService from "../services/WalletApiService";

export default {
  data() {
    return {
      item: {},
      wallet: "",
      BTCAddress: "",
      errors: [],
      Responses: []
    };
  },

  computed: {
    ...mapGetters(["BTCMoney"]),
    ...mapGetters(["fakeMoney"])
  },

  async mounted() {
    this.wallet = "real";
    await this.RefreshBTC();
    await this.RefreshFakeCoins();
    this.GetWalletAddress();
  },

  methods: {
    ...mapActions(["executeAsyncRequest"]),
    ...mapActions(["RefreshFakeCoins"]),
    ...mapActions(["RefreshBTC"]),

    async GetWalletAddress() {
      this.BTCAddress = await this.executeAsyncRequest(() =>
        WalletApiService.GetWalletAddress()
      );
    },

    async onSubmit(e) {
      e.preventDefault();

      var errors = [];
      if (this.fakeMoney >= 1000000)
        errors.push(
          "Le crédit est bloqué lorsque votre solde atteint 1,000,000 de bits"
        );
      this.errors = errors;

      if (errors.length == 0) {
        try {
          await this.executeAsyncRequest(() =>
            WalletApiService.AddCoins(this.item)
          );
          await this.RefreshFakeCoins();
        } catch (error) {}
      }
    },

    showModal() {
      var withdrawModal = document.getElementById("withdrawModal");
      withdrawModal.style.display = "block";
    },

    closeModal() {
        withdrawModal.style.display = "none";
    },

    async Withdraw(e) {
      e.preventDefault();
      var errors = [];
      if (this.item.AmountToSend < 100000)
        errors.push("Le retrait minimum est de 100,000 bits");
      else if (this.BTCMoney < this.item.AmountToSend)
        errors.push("Vous n'avez pas cette somme");


      this.errors = errors;
      if (errors.length === 0) {
        try {
          this.Responses = await this.executeAsyncRequest(() =>
            WalletApiService.Withdraw(this.item)
            
          );
          await this.RefreshBTC();
        } catch (error) {}
         if(Response != null)this.showModal();

      }
    },
 
    Copy() {
      navigator.clipboard.writeText(this.BTCAddress);
    },

    ChangeWallet(wallet) {
      this.wallet = wallet;
      this.errors = 0;
    }
  }
};
</script>

<style lang="scss">
// @import "compass/css3";

$body-bg: #c1bdba;
$form-bg: #13232f;
$white: #ffffff;

$main: #777c7b;
$main-light: lighten($main, 5%);
$main-dark: darken($main, 5%);

$gray-light: #a0b3b0;
$gray: #ddd;

$thin: 300;
$normal: 400;
$bold: 600;
$br: 4px;

.wallet html {
  overflow-y: scroll;
}

.wallet body {
  background: $body-bg;
  font-family: "Titillium Web", sans-serif;
}

.wallet a {
  text-decoration: none;
  color: $main;
  transition: 0.5s ease;
  &:hover {
    color: $main-dark;
  }
}

.wallet .form {
  background: rgba($form-bg, 0.9);
  padding: 40px;
  max-width: 600px;
  margin: 40px auto;
  border-radius: $br;
  box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
}

.wallet .tab-group {
  list-style: none;
  padding: 0;
  margin: 0 0 40px 0;
  &:after {
    content: "";
    display: table;
    clear: both;
  }
  li a {
    display: block;
    text-decoration: none;
    padding: 15px;
    background: rgba($gray-light, 0.25);
    color: $gray-light;
    font-size: 20px;
    float: left;
    width: 50%;
    text-align: center;
    cursor: pointer;
    transition: 0.5s ease;
    &:hover {
      background: $main-dark;
      color: $white;
    }
  }
  .active a {
    background: $main;
    color: $white;
  }
}

.tab-content > div:last-child {
  display: none;
}

.wallet h5 {
  text-align: center;
  color: $white;
  font-weight: $thin;
}

.wallet label {
  position: absolute;
  transform: translateY(6px);
  left: 13px;
  color: rgba($white, 0.5);
  transition: all 0.25s ease;
  -webkit-backface-visibility: hidden;
  pointer-events: none;
  font-size: 22px;
  .req {
    margin: 2px;
    color: #1ab188;
  }
}

.wallet .modal-header {
  padding: 10px 16px;
  text-align: center;
  background: #222222a8;
  color: white;
}

.wallet .modal-body {
  padding: 20px 16px;
  text-align: center;
  color : white;
}

.wallet .modal-footer {
  padding: 15px 16px;
  background-color: #222222a8;
  color: white;
}

.wallet .modal {
  display: none; /* Hidden by default */
  position: fixed; /* Stay in place */
  z-index: 1; /* Sit on top */
  padding-top: 16%; /* Location of the box */
  left: 0;
  top: 0;
  width: 100%; /* Full width */
  height: 100%; /* Full height */
  overflow: auto; /* Enable scroll if needed */
  background-color: rgb(0, 0, 0); /* Fallback color */
  background-color: rgba($body-bg, 0.4); /* Black w/ opacity */
}

/* Modal Content */
.wallet .modal-content {
  position: relative;
  background: rgba($form-bg, 0.9);
  margin: auto;
  padding: 0;
  width: 80%;
  box-shadow: 0 0 100px 50px rgba(0, 0, 0, 0.2),
    0 6px 20px 0 rgba(0, 0, 0, 0.19);
  -webkit-animation-name: animatetop;
  -webkit-animation-duration: 0.4s;
  animation-name: animatetop;
  animation-duration: 0.4s;
}

.wallet input,
textarea {
  font-size: 22px;
  display: block;
  width: 100%;
  height: 100%;
  padding: 5px 10px;
  background: none;
  background-image: none;
  border: 1px solid $gray-light;
  color: $white;
  border-radius: 0;
  transition: border-color 0.25s ease, box-shadow 0.25s ease;
  &:focus {
    outline: 0;
    border-color: $main;
  }
}

.wallet textarea {
  border: 2px solid $gray-light;
  resize: vertical;
}

.field-wrap {
  position: relative;
  margin-bottom: 10px;
}

.button {
  border: 0;
  outline: none;
  border-radius: 0;
  padding: 15px 0;
  font-size: 2rem;
  font-weight: $bold;
  text-transform: uppercase;
  letter-spacing: 0.1em;
  background: $main;
  color: $white;
  transition: all.5s ease;
  -webkit-appearance: none;
  &:hover,
  &:focus {
    background: $main-dark;
  }
}

.button-block {
  display: block;
  width: 100%;
}

.forgot {
  margin-top: -20px;
  text-align: right;
}
</style>
