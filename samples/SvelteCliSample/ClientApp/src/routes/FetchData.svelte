<script>
    import { onMount } from 'svelte';
    import SortableList from 'svelte-sortable-list';
    import Test from '../components/Test.svelte';
    import { selectedTests } from '../store/stores.js';

    export let params; // not used, exists to get rid of warning
    export let availableTests = [];
    let exportName = "TestList.json";

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
        <button on:click={handleExport}>Export</button>
        <label> File Name</label>
        <input type="text" bind:value={exportName} placeholder="file name"/>
    </form>

<div class="lists">
    <div>
        <h2>Available Tests</h2>

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
        <h2>Selected Tests</h2>

        {#if availableTests.length}
            <SortableList 
            {list} 
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
</div>


</div>

<style>
form{
    display:inline-block;
    margin-bottom: 4rem;
}
button{
    width: 10rem;
    height: 3rem;
    font-size: 1rem;
    border-radius: 4px;
}
input{
    width: 15rem;
    height: 2.2rem;
    font-size: 1rem;
    border-radius: 4px;

}

.lists{
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-around;
}
</style>