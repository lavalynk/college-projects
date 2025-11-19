# -----------------------------------------------------------------
# Assignment Name:  Assignment 3
# Name:             Keith Brock
# -----------------------------------------------------------------

import requests
import re

def decode_secret_message(url):
    # Download HTML from Google Docs
    response = requests.get(url)
    response.raise_for_status()
    html = response.text

    # Find all table rows <tr>...</tr>
    rows = re.findall(r'<tr.*?>(.*?)</tr>', html, re.S)

    coords = []
    for i, row in enumerate(rows):
        if i == 0:
            continue  # skip header row

        # Extract table cells <td>...</td>
        cells = re.findall(r'<td.*?>(.*?)</td>', row, re.S)
        if len(cells) >= 3:
            try:
                x = int(re.sub(r'<.*?>', '', cells[0]).strip())
                char = re.sub(r'<.*?>', '', cells[1]).strip() or " "
                y = int(re.sub(r'<.*?>', '', cells[2]).strip())
                coords.append((x, char, y))
            except ValueError:
                continue  # skip invalid rows

    if not coords:
        print("No valid coordinates found.")
        return

    # Find max dimensions
    max_x = max(x for x, _, _ in coords)
    max_y = max(y for _, _, y in coords)

    # Initialize grid with spaces
    grid = [[" " for _ in range(max_x + 1)] for _ in range(max_y + 1)]

    # Place characters
    for x, char, y in coords:
        grid[y][x] = char

    # Print grid
    for row in grid:
        print("".join(row))

# Example:
# decode_secret_message("https://docs.google.com/document/d/e/.../pub")



# Example usage:
decode_secret_message("https://docs.google.com/document/d/e/2PACX-1vRPzbNQcx5UriHSbZ-9vmsTow_R6RRe7eyAU60xIF9Dlz-vaHiHNO2TKgDi7jy4ZpTpNqM7EvEcfr_p/pub")


