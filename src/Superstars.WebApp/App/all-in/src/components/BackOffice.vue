<template>
<div class="page">
</div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import BackOfficeApiService from '../services/BackOfficeApiService';
import Vue from 'vue';

  export default {
    data() {
      return {
        item: {},
        wallet: "",
        BTCAddress: "",
        success :'',
        errors: [],
        Responses: []
      };
    },
        mounted() {
        
        },

        beforeDestroy() {

        },

        methods: {
            
        }
}
</script>

<style lang="scss">

</style>