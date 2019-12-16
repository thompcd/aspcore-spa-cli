<script>
    import { onMount } from 'svelte';
    import SortableList from 'svelte-sortable-list';
    import Test from '../components/Test.svelte';
    import { selectedTests } from '../store/stores.js';

    export let params; // not used, exists to get rid of warning
    export let availableTests = [];
    let exportName = "TestList";

    onMount(() => {
        fetch('/api/weatherforecast')
            .then(response => response.json())
            .then(data => {
                availableTests = data;
            });
    });

    let list;
    $: availableTests = availableTests.map((obj, i) => ({ ...obj, id: i}));
    $: console.log("lists: ", list);
    $: console.log("availableTests: ", availableTests);

    const unsubscribe = selectedTests.subscribe( value => {
        list = value;
    });

    const sortList = ev => {list = ev.detail};

    const handleExport = () => { 
        downloadObjectAsJson(list, exportName);
    };

   function downloadObjectAsJson(exportObj, exportName){
    var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(exportObj));
    var downloadAnchorNode = document.createElement('a');
    downloadAnchorNode.setAttribute("href",     dataStr);
    downloadAnchorNode.setAttribute("download", exportName + ".json");
    document.body.appendChild(downloadAnchorNode); // required for firefox
    downloadAnchorNode.click();
    downloadAnchorNode.remove();
  }

</script>

<div>
    <h1>Test Builder</h1>
    <form>
        <button type="button" class="btn btn-secondary" on:click={handleExport}>Create Test List</button>
        <label> File Name</label>
        <input type="text" bind:value={exportName} placeholder="file name"/>
        <small id="emailHelp" class="form-text text-muted">Use this to save files for production.</small>
    </form>

<div class="lists">
    <div>
        <h2>Available Tests</h2>
        <small id="availableTestHelp" class="form-text text-muted">The are the default tests supported at the moment.<br>Feel free to edit some limits and add them to your current test list.</small>

        {#if availableTests.length}
            <SortableList 
            list={availableTests} 
            key="id" 
            on:sort={sortList}
            let:item
            let:index >
                <Test {item} {index}/>
            </SortableList>
        {:else}
            <p><em>Loading...</em></p>
        {/if}
    </div>
        <div>
        <h2 class="text-warning">Selected Tests</h2>
        <small id="availableTestHelp" class="form-text text-muted">These are the tests that get saved. Make sure they're unique!</small>

        {#if availableTests.length}
            <SortableList 
            class="list-group"
            {list} 
            key="id" 
            on:sort={sortList}
            let:item
            let:index >
                <Test class="list-group-item" {item} {index}/>
            </SortableList>
        {:else}
            <p><em>Loading...</em></p>
        {/if}
    </div>
</div>


</div>

<style>
form{
    display:inline-block;
    margin-bottom: 4rem;
}


.lists{
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-around;
}
</style>