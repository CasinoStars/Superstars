<template>
  <div class="sc-message--text" :style="messageColors">
    <p v-html="messageText"></p>
    <p v-if="data.meta" class='sc-message--meta' :style="{color: messageColors.color}">{{data.meta}}</p>
  </div>
</template>

<script>
import Autolinker from 'autolinker'

export default {
  props: {
    data: {
      type: String,
      required: true
    },
    messageColors: {
      type: Object,
      required: true
    }
  },
  computed: {
    messageText() {
      return Autolinker.link(this.data
        .replace(/&/g, '&amp;')
	      .replace(/"/g, '&quot;')
	      .replace(/'/g, '&#39;')
	      .replace(/</g, '&lt;')
	      .replace(/>/g, '&gt;'), {
        className: 'chatLink',
        truncate: { length: 50, location: 'smart' }
      })
    }
  }
}
</script>

<style scoped>
a.chatLink {
  color: inherit !important;
}
</style>
