# Skeleton
The <FluentSkeleton> component is used as a visual placeholder for an element while it is in a loading state and usually presents itself as a simplified wireframe-like version of the UI it is representing.

<FluentSkeleton> wraps the <fluent-skeleton> element, a web component implementation of a radio element leveraging the Fluent UI design system.

## Examples
### Element blocks

``` C#
<FluentCard class="card-padding">
    <FluentSkeleton Shape="SkeletonShape.Circle"></FluentSkeleton>
    <FluentSkeleton Style="margin-top: 10px" Height="10px;"></FluentSkeleton>
    <FluentSkeleton Style="margin-top: 10px" Height="10px;"></FluentSkeleton>
    <FluentSkeleton Style="margin-top: 10px" Height="10px;"></FluentSkeleton>
    <FluentSkeleton Style="margin-top: 20px;margin-bottom: 10px;" Width="75px" Height="30px"></FluentSkeleton>
</FluentCard>
```

### Element blocks with shimmer effect

This example uses an isolated CSS file to define margins.

``` C#
<div>
    <FluentCard Class="card-padding">
        <FluentSkeleton Shape="SkeletonShape.Circle" Shimmer="true"></FluentSkeleton>
        <FluentSkeleton Height="10px" Shimmer="true"></FluentSkeleton>
        <FluentSkeleton Height="10px" Shimmer="true"></FluentSkeleton>
        <FluentSkeleton Height="10px" Shimmer="true"></FluentSkeleton>
        <FluentSkeleton Width="75px" Height="30px" Shimmer="true"></FluentSkeleton>
    </FluentCard>
</div>
```

``` CCS
::deep .card-padding fluent-skeleton:not(:first-child) {
    margin-top: 10px;
}

::deep .card-padding fluent-skeleton:last-child {
    margin-top: 20px;
    margin-bottom: 10px;
}
```

### Using SVG via Pattern attribute

``` C#
<FluentCard>
    <FluentSkeleton Width="500px"
                    Height="250px"
                    Pattern="./_content/FluentUI.Demo.Shared/images/skeleton-test-pattern.svg"
                    Shimmer="true"></FluentSkeleton>
</FluentCard>
```

### Using inline SVG

``` C#
<FluentCard>
    <FluentSkeleton Width="500px" Height="250px" Shimmer="true">
        <svg style="position: absolute; left: 0; top: 0;" id="pattern" width="100%" height="100%">
            <defs>
                <mask id="mask" x="0" y="0" width="100%" height="100%">
                    <rect x="0" y="0" width="100%" height="100%" fill="#ffffff" />
                    <rect x="0" y="0" width="100%" height="45%" rx="4" />
                    <rect x="25" y="55%" width="90%" height="15px" rx="4" />
                    <rect x="25" y="65%" width="70%" height="15px" rx="4" />
                    <rect x="25" y="80%" width="90px" height="30px" rx="4" />
                </mask>
            </defs>
            <rect x="0" y="0" width="100%" height="100%" mask="url(#mask)" fill="#ffffff" />
        </svg>
    </FluentSkeleton>
</FluentCard>
```

## Documentation
### FluentSkeleton Class
#### Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| ChildContent | RenderFragment? |  | Gets or sets the content to be rendered inside the component. |
| Fill | string? |  | Indicates the Skeleton should have a filled style. |
| Height | string | 50px | Gets or sets the height of the skeleton. |
| Pattern | string? |  | Gets or sets the skeleton pattern. |
| Shape | SkeletonShape? | Rect | Gets or sets the shape of the skeleton. See SkeletonShape |
| Shimmer | bool? |  | Gets or sets a value indicating whether the skeleton is shimmered. |
| Visible | bool | True | Gets or sets a value indicating whether the skeleton is visible. |
| Width | string | 50px | Gets or sets the width of the skeleton. |
