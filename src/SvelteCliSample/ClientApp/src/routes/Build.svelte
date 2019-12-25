<script>
    import { onMount } from 'svelte';
    import SortableList from 'svelte-sortable-list';
    import Test from '../components/Test.svelte';
    import { availableTests } from '../store/tests.js'
    


    let params;
    let exportName = "TestList";
    let selectedTests = []

    const sortSelectedList = ev => { selectedTests = (ev.detail) };
    const handleExport = () => { downloadObjectAsJson(selectedTests, exportName) };


    function addTest(item) {
        const retVal = selectedTests.find( obj => obj === item)

        if (retVal){
            alert(`${item.testName} is already selected`)
            return
        }

        selectedTests.push(item)
        selectedTests = selectedTests;
    }

    function removeTest(item) {
        const objIndex = selectedTests.findIndex( obj => obj === item)

        if (objIndex > -1) {
            selectedTests.splice(objIndex, 1);
            selectedTests = selectedTests;
        }
    }

    function downloadObjectAsJson(exportObj, exportName){
        var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(exportObj, null, "\t"));
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
    <div class="tests" ref="tests">
        <h2>Available Tests</h2>
        <small class="form-text text-muted section">These are the default tests supported at the moment.<br>Feel free to edit some limits and add them to your current test list.</small>

        {#if $availableTests}
            {#each $availableTests as item, index}
            <Test {item} {index} collapsible={true}>
                <button slot="actionButton" type="button" class="btn btn-warning" on:click={()=> addTest(item)}>
                Add To Selected Tests
                </button>
            </Test>
            {/each}
        {:else}
            <p><em>Loading...</em></p>
        {/if}
    </div>
    <div class="tests">
        <h2 class="text-warning">Selected Tests</h2>
        <small class="form-text text-muted section">These are the tests that get saved.<br>Drag them into the order you want to test them!</small>

        {#if selectedTests.length}
            <SortableList 
            bind:list={selectedTests} 
                key="id" 
            on:sort={sortSelectedList}
            let:item
            let:index >
                <Test {item} {index} collapsible={false}>
                    <span slot="indexNumber" class="orderIndex">{index+1}</span>
                    <button slot="actionButton" type="button" class="btn btn-danger" on:click={()=> removeTest(item)}>
                    Remove From Selected Tests
                    </button>
                </Test>
            </SortableList>
        {:else}
            <p style="text-align:center;"><em>No tests selected</em></p>
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
.tests{
    width: 33rem;
}
.section{
    /* fixed subtext height for alignment, check you don't have text collision if you adjust this */
    height: 2.2rem; 
    margin-bottom: .8rem;
    text-overflow: '';
    overflow: hidden;
}

small{
    padding-bottom: 2rem;
}

.btn-danger:hover{
    border: solid 1px rgba(220, 53, 69, 1);
    box-shadow: 0 5px 15px rgba(220, 53, 69, 1);
}

.orderIndex{
    display: inline-block;
    align-content: center;
    background-color: #ddd;
    border-radius: 4px;
    width: 4rem;
    text-align: center;
}
</style>