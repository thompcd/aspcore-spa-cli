<script>
	import { selectedTests } from '../store/stores.js';

	export const addTest = n => {

		selectedTests.set(n => (console.log("defined?" , n)));
	}
</script>

<button on:click={addTest}>
	+
</button>