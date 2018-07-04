<template>
    <div id="home">
    <header class="ich" style="padding:128px 16px;">
        <div style="text-align:center; letter-spacing: 1px; font-family: 'Courier New', sans-serif;"> 
            <h1>WELCOME ON ALL`IN</h1>
        </div>
    </header>

<center>
<strong><a style="font-size: 35px;   font-family: 'Courier New', sans-serif; font-variant: small-caps;"> BANQUE DU SITE - {{BTCBankCoins}} <i class="fa fa-btc" style="font-size: 35px;"></i> | {{fakeBankCoins}} <i class="fa fa-money" style="font-size: 35px;"></i> </a></strong>
</center>

    </div>

</template>

<script>
import { mapActions } from 'vuex';
import Vue from 'vue';
import WalletApiService from '../services/WalletApiService';

export default {
  
  data(){
    return {
      BTCBankCoins: 0,
      fakeBankCoins: 0,
    }
  },

     async created(){
      await this.BTCBank();
      await this.fakeBank();
    },

     methods: {
    ...mapActions(['executeAsyncRequest']),
    async BTCBank() {
      this.BTCBankCoins = await this.executeAsyncRequest(() => WalletApiService.GetBTCBankRoll());
    },

    async fakeBank() {
      this.fakeBankCoins = await this.executeAsyncRequest(() => WalletApiService.GetFakeBankRoll());
    },

     } 
}
</script>

<style lang="css">



</style>


  