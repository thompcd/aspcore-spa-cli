<script>
	import { selectedTests } from '../store/stores.js';

	export const addTest = n => {
		selectedTests.set(n => (console.log("defined?" , n)));
	}
</script>

<button type="button" class="btn btn-warning" on:click={addTest}>
	Add to Selected Tests
</button>