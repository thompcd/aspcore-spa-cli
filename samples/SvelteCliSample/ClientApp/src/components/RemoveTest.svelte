<script>
	import { selectedTests } from '../store/stores.js';

	export const removeTest = n => {
        console.log("about to remove test")
		selectedTests.set(n => (console.log("defined?" , n)));
	}
</script>

<button type="button" class="btn btn-danger" on:click={removeTest}>
	Remove from Selected Tests
</button>